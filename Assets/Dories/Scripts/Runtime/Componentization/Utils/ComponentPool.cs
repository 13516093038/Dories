using System;
using System.Collections.Concurrent;

namespace Dories.Runtime.Componentization.Utils
{
    public class ComponentPool
    {
        public Type Type { get; internal set; }
        
        private ConcurrentQueue<Component> m_Components = new ConcurrentQueue<Component>();
        

        protected internal Component Acquire(object userData = null)
        {
            m_Components.TryDequeue(out var component);
            if (component == null)
            {
                component = Activator.CreateInstance(Type) as Component;
            }
            component.Id = ComponentIdGenerator.Id;
            component.OnAcquire(userData);
           
            return component;
        }

        protected internal Component Acquire(Component parent, object userData = null)
        {
            m_Components.TryDequeue(out var component);
            if (component == null)
            {
                component = Activator.CreateInstance(Type) as Component;
            }
            component.Parent = parent;
            component.Id = ComponentIdGenerator.Id;
            
            component.OnAcquire(userData);
           
            return component;
        }

        protected internal void Release(Component component, object userData = null)
        {
            if (component == null)
            {
                throw new Exception("Cannot release a null component.");
            }
            else if (component.GetType() != Type)
            {
                throw new Exception("Cannot release a non-component component.");
            }
            
            component.Dispose();
            component.Id = 0;
            component.Parent = null;
           
            
            m_Components.Enqueue(component);
        }

        protected internal void Destroy(Component component, object userData = null)
        {
            if (component == null)
            {
                throw new Exception("Cannot release a null component.");
            }
            else if (component.GetType() != Type)
            {
                throw new Exception("Cannot release a non-component component.");
            }
            
            component.Dispose();
            component.Id = 0;
            component.Parent = null;
        }
    }
}