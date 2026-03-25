using YooAsset;

namespace Dories.Base.Patch.Runtime.Operations.InitPackageOperation
{
    public class HostPlayInitOperation : IYooAssetInitOperation
    {
        public InitializationOperation Init(ResourcePackage package, string packageName, IRemoteServices remoteServices)
        {
            var createParameters = new HostPlayModeParameters();
            createParameters.BuildinFileSystemParameters =
                FileSystemParameters.CreateDefaultBuildinFileSystemParameters();
            createParameters.CacheFileSystemParameters =
                FileSystemParameters.CreateDefaultCacheFileSystemParameters(remoteServices);
            return package.InitializeAsync(createParameters);
        }
    }
}