using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dories.Runtime.Componentization.Utils
{
    public static partial class ComponentFactory
    {
        private static Dictionary<Type, ISingleton> m_SingletonComponents = new Dictionary<Type, ISingleton>();

        public static void RegisterSingletonComponent<T>(T singleton) where T : ISingleton
        {
            if(m_SingletonComponents.ContainsKey(typeof(T)))
            {
                Debug.LogError($"Singleton component {typeof(T)} already registered");
                return;
            }
            m_SingletonComponents.Add(typeof(T), singleton);
        }

        public static void UnregisterSingletonComponent<T>() where T : ISingleton
        {
            if(!m_SingletonComponents.ContainsKey(typeof(T)))
            {
                Debug.LogError($"Singleton component {typeof(T)} not registered");
                return;
            }
            m_SingletonComponents.Remove(typeof(T));
        }

        public static T GetSingletonComponent<T>() where T : ISingleton
        {
            if(!m_SingletonComponents.TryGetValue(typeof(T), out var singleton))
            {
                Debug.LogError($"Singleton component {typeof(T)} not registered");
                return default(T);
            }
            return (T)singleton;
        }
    }
}