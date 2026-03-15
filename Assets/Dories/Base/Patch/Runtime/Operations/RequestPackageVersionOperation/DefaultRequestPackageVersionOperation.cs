using YooAsset;

namespace Dories.Base.Patch.Runtime.Operations.RequestPackageVersionOperation
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