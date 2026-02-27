using YooAsset;

namespace Dories.Runtime.YooAssetResourceSystem.Operations.InitPackageOperation
{
    public interface IYooAssetInitOperation
    {
        InitializationOperation Init(ResourcePackage package,string packageName);
    }
}