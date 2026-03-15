using YooAsset;

namespace Dories.Base.Patch.Runtime.Operations.UpdatePackageManifestOperation
{
    public interface IYooAssetUpdatePackageManifestOperation
    {
        YooAsset.UpdatePackageManifestOperation UpdatePackageManifest(ResourcePackage package, string packageVersion);
    }
}