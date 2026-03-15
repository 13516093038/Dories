using YooAsset;

namespace Dories.Base.Patch.Runtime.Operations.RequestPackageVersionOperation
{
    public interface IYooAssetRequestPackageVersionOperation
    {
        YooAsset.RequestPackageVersionOperation RequestPackageVersion(ResourcePackage package);
    }
}