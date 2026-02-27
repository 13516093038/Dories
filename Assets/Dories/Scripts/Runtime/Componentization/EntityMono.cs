using System;
using UnityEngine;

namespace Dories.Runtime.Componentization.Utils
{
    public class EntityMono : MonoBehaviour, IDisposable
    {
        //Entity实体
        private Entity m_Entity;
        
        public virtual Entity Entity => m_Entity;

        public virtual void SetEntity(Entity entity)
        {
            m_Entity = entity;
        }

        private void InternalCheck()
        {
            if (m_Entity == null)
            {
                throw new Exception("Entity is null");
            }
        }
        
        /// <summary>
        /// 添加子组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T AddComponent<T>(object userData = null) where T : Component
        {
            InternalCheck();
            return m_Entity.AddComponent<T>(userData);
        }

        /// <summary>
        /// 添加子组件
        /// </summary>
        /// <param name="component"></param>
        /// <exception cref="Exception"></exception>
        public void AddComponent(Component component)
        {
            InternalCheck();
            m_Entity.AddComponent(component);
        }
        
        /// <summary>
        /// 移除子组件
        /// </summary>
        /// <param name="component"></param>
        /// <param name="isRelease"></param>
        /// <exception cref="Exception"></exception>
        public void RemoveComponent(Component component, bool isRelease = true)
        {
            InternalCheck();
            m_Entity.RemoveComponent(component, isRelease);
        }

        /// <summary>
        /// 获取指定类型的第一个子组件
        /// </summary>
        /// <param name="componentType"></param>
        /// <returns></returns>
        public Component GetComponentCSharp(Type componentType)
        {
            InternalCheck();
            return m_Entity.GetComponent(componentType);
        }

        /// <summary>
        /// 获取指定类型的第index个子组件
        /// </summary>
        /// <param name="componentType"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public Component GetComponent(Type componentType, int index)
        {
            InternalCheck();
            return m_Entity.GetComponent(componentType, index);
        }

        /// <summary>
        /// 获取指定类型的第一个子组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetComponentCSharp<T>() where T : Component
        {
            InternalCheck();
            return m_Entity.GetComponent<T>();
        }

        /// <summary>
        /// 获取指定类型的第index个子组件
        /// </summary>
        /// <param name="index"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetComponent<T>(int index) where T : Component
        {
            InternalCheck();
            return m_Entity.GetComponent<T>(index);
        }

        public void Dispose()
        {
            m_Entity?.Dispose();
        }
    }
}