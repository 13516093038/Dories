using YooAsset;

namespace Dories.Runtime.YooAssetResourceSystem.Operations.RequestPackageVersionOperation
{
    public interface IYooAssetRequestPackageVersionOperation
    {
        YooAsset.RequestPackageVersionOperation RequestPackageVersion(ResourcePackage package);
    }
}