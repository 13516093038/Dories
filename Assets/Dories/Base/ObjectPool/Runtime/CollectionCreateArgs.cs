using Dories.Base.Reference.Runtime;
using Dories.Base.AssetLoader.Runtime;

namespace Dories.Base.ObjectPool.Runtime
{
    internal class CollectionCreateArgs : IReference
    {
        public IAssetLoader AssetLoader { get; private set; }
        public int PrewarmCount { get; private set; }
        public string DefaultPath { get;  private set; }
        
        public object PrewarmData { get;  private set; }

        
        public static CollectionCreateArgs Create(IAssetLoader assetLoader, string defaultPath, int prewarmCount = 0, object prewarmData = null)
        {
            var args = ReferencePool.Acquire<CollectionCreateArgs>();
            args.PrewarmCount =  prewarmCount;
            args.AssetLoader = assetLoader;
            args.DefaultPath = defaultPath;
            args.PrewarmData = prewarmData;
            return args;
        }
        
        public void Dispose()
        {
            AssetLoader  = null;
            PrewarmCount = 0;
        }

        public void Release()
        {
            ReferencePool.Release(this);
        }
    }
}