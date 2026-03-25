using YooAsset;

namespace Dories.Base.Patch.Runtime.Operations.InitPackageOperation
{
    public class OfflineInitOperation : IYooAssetInitOperation
    {
        public InitializationOperation Init(ResourcePackage package, string packageName, IRemoteServices remoteServices)
        {
            var createParameters = new OfflinePlayModeParameters();
            createParameters.BuildinFileSystemParameters =
                FileSystemParameters.CreateDefaultBuildinFileSystemParameters();
            return package.InitializeAsync(createParameters);
        }
    }
}