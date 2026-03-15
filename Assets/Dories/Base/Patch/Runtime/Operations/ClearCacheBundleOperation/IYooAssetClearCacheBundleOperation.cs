using YooAsset;

namespace Dories.Base.Patch.Runtime.Operations.ClearCacheBundleOperation
{
    public interface IYooAssetClearCacheBundleOperation
    {
       ClearCacheFilesOperation ClearCacheBundle(ResourcePackage package);
    }
}