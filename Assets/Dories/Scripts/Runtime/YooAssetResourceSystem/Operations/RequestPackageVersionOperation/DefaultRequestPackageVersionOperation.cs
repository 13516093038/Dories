using YooAsset;

namespace Dories.Runtime.YooAssetResourceSystem.Operations.RequestPackageVersionOperation
{
    public class DefaultRequestPackageVersionOperation : IYooAssetRequestPackageVersionOperation
    {
        public YooAsset.RequestPackageVersionOperation RequestPackageVersion(ResourcePackage package)
        {
            var operation = package.RequestPackageVersionAsync();
            return operation;
        }
    }
}