using System;
using Dories.Base.Componentization.Runtime;

namespace Dories.Base.Fsm.Runtime
{
    public class StateBase<T> : Entity where T : IFsmOwner
    {
        protected internal T Owner { get; set; }
        protected internal FsmSystem FsmSystem { get; set; }

        public virtual void OnInit()
        {

        }

        public virtual void OnEnter()
        {
           
        }

        public virtual void OnExit()
        {
            
        }

        protected void ChangeState(Type nextStateType)
        {
            FsmSystem.ChangeState(Owner, nextStateType);
        }

        protected void ChangeState<TK>() where TK : StateBase<T>
        {
            ChangeState(typeof(TK));
        }
    }
}