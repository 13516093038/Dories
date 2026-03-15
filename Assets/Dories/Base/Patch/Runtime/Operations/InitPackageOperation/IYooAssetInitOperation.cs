using YooAsset;

namespace Dories.Base.Patch.Runtime.Operations.InitPackageOperation
{
    public interface IYooAssetInitOperation
    {
        InitializationOperation Init(ResourcePackage package,string packageName);
    }
}