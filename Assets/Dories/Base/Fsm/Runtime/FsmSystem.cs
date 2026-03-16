using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Dories.Base.Componentization.Runtime;
using Dories.Base.Componentization.Runtime.Utils;
using Dories.Base.Reference.Runtime;

namespace Dories.Base.Fsm.Runtime
{
  internal class FsmInfo<T> : IReference where T : IFsmOwner
  {
    public T Owner { get; internal set; }
    public List<StateBase<T>> States { get; internal set; }
    public StateBase<T> CurrentState { get; internal set; }
    public bool IsRunning { get; internal set; }

    public void Dispose()
    {
      Owner = default(T);
      States.Clear();
      States = null;
      CurrentState = null;
      IsRunning = false;
    }
  }

  public class FsmSystem : Entity
  {
    private Dictionary<IFsmOwner, IReference> m_FsmInfos = new Dictionary<IFsmOwner, IReference>();

    /// <summary>
    /// Create a new Fsm for the owner
    /// </summary>
    /// <param name="owner">The owner of the Fsm</param>
    /// <param name="stateTypes">The types of the states</param>
    /// <typeparam name="T">The type of the owner</typeparam>
    public void CreateFsm<T>(T owner, List<Type> stateTypes) where T : IFsmOwner
    {
      if (m_FsmInfos.ContainsKey(owner))
      {
        Debug.LogError($"Fsm already exists for owner {owner.GetType()}");
        return;
      }

      var fsmInfo = ReferencePool.Acquire<FsmInfo<T>>();
      fsmInfo.Owner = owner;
      fsmInfo.States = new List<StateBase<T>>();
      foreach (var stateType in stateTypes)
      {
        var state = ComponentFactory.Acquire(stateType) as StateBase<T>;
        state.Owner = owner;
        state.FsmSystem = this;
        state.OnInit();
        fsmInfo.States.Add(state);
      }

      fsmInfo.IsRunning = false;

      foreach (var state in fsmInfo.States)
      {
        state.Owner = owner;
        state.OnInit();
      }

      m_FsmInfos.Add(owner, fsmInfo);
    }

    /// <summary>
    /// Start the Fsm for the owner
    /// </summary>
    /// <param name="owner">The owner of the Fsm</param>
    /// <param name="startStateType">The type of the start state</param>
    /// <typeparam name="T">The type of the owner</typeparam>
    public void StartFsm<T>(T owner, Type startStateType) where T : IFsmOwner
    {
      if (!m_FsmInfos.TryGetValue(owner, out var reference))
      {
        Debug.LogError($"Fsm not found for owner {owner.GetType()}");
        return;
      }

      var fsmInfo = m_FsmInfos[owner] as FsmInfo<T>;
      var startState = fsmInfo.States.FirstOrDefault(state => state.GetType() == startStateType);
      if (startState == null)
      {
        Debug.LogError($"Start state not found for owner {owner.GetType()}");
        return;
      }

      fsmInfo.CurrentState = startState;
      startState.OnEnter();
      fsmInfo.IsRunning = true;
    }

    /// <summary>
    /// Destroy the Fsm for the owner
    /// </summary>
    /// <param name="owner">The owner of the Fsm</param>
    /// <typeparam name="T">The type of the owner</typeparam>
    public void DestroyFsm<T>(T owner) where T : IFsmOwner
    {
      if (!m_FsmInfos.TryGetValue(owner, out var reference))
      {
        Debug.LogError($"Fsm not found for owner {owner.GetType()}");
        return;
      }

      var fsmInfo = m_FsmInfos[owner] as FsmInfo<T>;
      fsmInfo.CurrentState?.OnExit();
      fsmInfo.IsRunning = false;
      foreach (var state in fsmInfo.States)
      {
        ComponentFactory.Release(state);
      }

      fsmInfo.States.Clear();
      m_FsmInfos.Remove(owner);
      ReferencePool.Release(fsmInfo);
    }

    /// <summary>
    /// Change the state of the Fsm for the owner
    /// </summary>
    /// <param name="owner">The owner of the Fsm</param>
    /// <param name="nextStateType">The type of the next state</param>
    /// <typeparam name="T">The type of the owner</typeparam>
    public void ChangeState<T>(T owner, Type nextStateType) where T : IFsmOwner
    {
      if (!m_FsmInfos.TryGetValue(owner, out var reference))
      {
        Debug.LogError($"Fsm not found for owner {owner.GetType()}");
        return;
      }

      var fsmInfo = m_FsmInfos[owner] as FsmInfo<T>;
      fsmInfo.CurrentState?.OnExit();
      fsmInfo.CurrentState = fsmInfo.States.FirstOrDefault(state => state.GetType() == nextStateType);
      fsmInfo.CurrentState?.OnEnter();
    }
  }
}