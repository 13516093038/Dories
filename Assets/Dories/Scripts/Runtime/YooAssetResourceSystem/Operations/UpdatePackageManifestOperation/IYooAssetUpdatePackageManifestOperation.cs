using YooAsset;

namespace Dories.Runtime.YooAssetResourceSystem.Operations.UpdatePackageManifestOperation
{
    public interface IYooAssetUpdatePackageManifestOperation
    {
        YooAsset.UpdatePackageManifestOperation UpdatePackageManifest(ResourcePackage package, string packageVersion);
    }
}