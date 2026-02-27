using Dories.Runtime.Reference;
using Dories.Runtime.ResourceProvider;

namespace Dories.Runtime.ObjectPool
{
    internal class CollectionCreateArgs : IReference
    {
        public IResourceProvider ResourceProvider { get; private set; }
        public int PrewarmCount { get; private set; }
        public string DefaultPath { get;  private set; }
        
        public object PrewarmData { get;  private set; }

        
        public static CollectionCreateArgs Create(IResourceProvider resourceProvider, string defaultPath, int prewarmCount = 0, object prewarmData = null)
        {
            var args = ReferencePool.Acquire<CollectionCreateArgs>();
            args.PrewarmCount =  prewarmCount;
            args.ResourceProvider = resourceProvider;
            args.DefaultPath = defaultPath;
            args.PrewarmData = prewarmData;
            return args;
        }
        
        public void Dispose()
        {
            ResourceProvider  = null;
            PrewarmCount = 0;
        }

        public void Release()
        {
            ReferencePool.Release(this);
        }
    }
}