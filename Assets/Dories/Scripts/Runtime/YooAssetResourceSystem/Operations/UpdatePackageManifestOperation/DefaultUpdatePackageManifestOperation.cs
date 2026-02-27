using YooAsset;

namespace Dories.Runtime.YooAssetResourceSystem.Operations.UpdatePackageManifestOperation
{
    public class DefaultUpdatePackageManifestOperation : IYooAssetUpdatePackageManifestOperation
    {
        public YooAsset.UpdatePackageManifestOperation UpdatePackageManifest(ResourcePackage package, string packageVersion)
        {
            return package.UpdatePackageManifestAsync(packageVersion);
        }
    }
}