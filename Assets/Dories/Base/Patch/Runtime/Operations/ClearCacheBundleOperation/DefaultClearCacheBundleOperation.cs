using YooAsset;

namespace Dories.Base.Patch.Runtime.Operations.ClearCacheBundleOperation
{
    public class DefaultClearCacheBundleOperation : IYooAssetClearCacheBundleOperation
    {
        public ClearCacheFilesOperation ClearCacheBundle(ResourcePackage package)
        {
            var operation = package.ClearCacheFilesAsync(EFileClearMode.ClearUnusedBundleFiles);
            return operation;
        }
    }
}