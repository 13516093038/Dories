using Dories.Base.Patch.Runtime.YooAssetExtensions.OperationExtensions;
using YooAsset;

namespace Dories.Base.Patch.Runtime.Operations.RequestPackageVersionOperation
{
    public class WeakOnlineRequestPackageVersionOperation : IYooAssetRequestPackageVersionOperation
    {
        public YooAsset.RequestPackageVersionOperation RequestPackageVersion(ResourcePackage package)
        {
            var operation = new WeakOnlineRequestPackageVersionHelper(package);
            operation.StartOperation();
            return operation;
        }
    }
}


