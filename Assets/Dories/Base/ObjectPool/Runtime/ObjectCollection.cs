using System;
using System.Collections.Concurrent;
using Cysharp.Threading.Tasks;
using Dories.Base.Componentization.Runtime;
using Dories.Base.AssetLoader.Runtime;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Dories.Base.ObjectPool.Runtime
{
    internal class ObjectCollection<T> : Entity where T : PoolEntity
    {
        private ConcurrentQueue<T> m_Queue = new ConcurrentQueue<T>();
        private IAssetLoader m_AssetLoader;
        private Type m_PoolType;
        private int m_PrewarmCount;
        private string m_DefaultPath;

        private GameObject m_PoolCollector;
        private GameObject m_Prefab;

        protected internal override async void OnAcquire(object userData = null)
        {
            base.OnAcquire(userData);
            
            var args = userData as CollectionCreateArgs;
            
            m_AssetLoader = args.AssetLoader;
            m_PoolType = typeof(T);
            m_PrewarmCount  = args.PrewarmCount;
            m_DefaultPath  = args.DefaultPath;
            
            m_Queue  = new ConcurrentQueue<T>();
            m_PoolCollector = new GameObject(m_PoolType.Name);

            await Prewarm(args.PrewarmData);
            args.Release();
        }

        protected internal async UniTask<T> Acquire(object userData = null, Transform parent = null)
        {
            await LoadPrefab();
            if (!m_Queue.TryDequeue(out var entity))
            {
                entity = Object.Instantiate(m_Prefab).GetComponent<T>();
                entity.transform.SetParent(parent == null ? m_PoolCollector.transform : parent);
                entity.PoolId = GetHashCode();
                entity.OnInit(userData);
            }
            entity.OnOpen(userData);
            entity.gameObject.SetActive(true);
            return entity;
        }

        protected internal void Release(T entity, object userData = null)
        {
            if (entity.GetType() != typeof(T))
            {
                throw new Exception("entity type is not " + typeof(T).Name);
            }
            
            entity.OnClose(userData);
            entity.gameObject.SetActive(false);
            entity.transform.SetParent(m_PoolCollector.transform);
            m_Queue.Enqueue(entity);
        }

        private async UniTask Prewarm(object userData)
        {
            await LoadPrefab();
            for (int i = 0; i < m_PrewarmCount; i++)
            {
                var entity = Object.Instantiate(m_Prefab).GetComponent<T>();
                entity.gameObject.SetActive(false);
                entity.transform.SetParent(m_PoolCollector.transform);
                entity.PoolId = GetHashCode();
                entity.OnInit(userData);
                m_Queue.Enqueue(entity);
            }
        }

        private async UniTask LoadPrefab()
        {
            if (!m_Prefab)
            {
                m_Prefab = await m_AssetLoader.LoadResourceAsync<GameObject>(m_DefaultPath);
            }
        }

        private void Destroy()
        {
            foreach (var entity in m_Queue)
            {
                entity.OnRecycle();
                Object.Destroy(entity);
            }
            m_Queue.Clear();
            Object.Destroy(m_PoolCollector);
            Object.Destroy(m_Prefab);
        }

        public override void Dispose()
        {
            base.Dispose();
            Destroy();
        }
    }
}