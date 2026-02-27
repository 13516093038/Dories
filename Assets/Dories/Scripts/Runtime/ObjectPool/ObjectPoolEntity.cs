using System;
using System.Collections.Concurrent;
using Cysharp.Threading.Tasks;
using Dories.Runtime.Componentization;
using Dories.Runtime.Componentization.Utils;
using Dories.Runtime.ResourceProvider;

namespace Dories.Runtime.ObjectPool
{
    public class ObjectPoolEntity : Entity
    {
        private IResourceProvider m_ResourceProvider;

        private ConcurrentDictionary<string, Component> m_EntityCollection;

        protected internal override void OnAcquire(object userData = null)
        {
            base.OnAcquire(userData);
            
            m_ResourceProvider = userData as IResourceProvider;
            m_EntityCollection =  new ConcurrentDictionary<string, Component>();
        }

        public void CreatePool<T>(string path, int prewarmCount = 0, object prewarmData = null) where T : PoolEntity
        {
            InternalCheck(path);
            m_EntityCollection.TryAdd(path,
                AddComponent<ObjectCollection<T>>(CollectionCreateArgs.Create(m_ResourceProvider, path, prewarmCount,
                    prewarmData)));
        }

        public async UniTask<T> Acquire<T>(string path, object userData) where T : PoolEntity
        {
            return await InternalGetCollection<T>(path).Acquire(userData);
        }

        public void Release<T>(T poolEntity) where T : PoolEntity
        {
            InternalGetCollection<T>(poolEntity.PoolId).Release(poolEntity);
        }

        public void DestroyPool(string path)
        {
            if (!m_EntityCollection.TryGetValue(path, out var value))
            {
                throw new Exception("Pool doesn't exists: " + path);
            }

            RemoveComponent(value);
        }

        public void DestroyAllPool()
        {
            foreach (var item in m_EntityCollection)
            {
                RemoveComponent(item.Value);
            }
        }

        private void InternalCheck(string path)
        {
            if (m_EntityCollection.ContainsKey(path))
            {
                throw new Exception("Pool already exists: " + path);
            }
        }

        private ObjectCollection<T> InternalGetCollection<T>(string path) where T : PoolEntity
        {
            if (!m_EntityCollection.TryGetValue(path, out var value))
            {
                throw new Exception("Pool doesn't exists: " + path);
            }
            return value as ObjectCollection<T>;
        }
        
        private ObjectCollection<T> InternalGetCollection<T>(int poolId) where T : PoolEntity
        {
            foreach (var collector in m_EntityCollection)
            {
                if (poolId == collector.Value.GetHashCode())
                {
                    return collector.Value as ObjectCollection<T>;
                }
            }
            throw new Exception("Pool doesn't exists: " + poolId);
        }
    }
}