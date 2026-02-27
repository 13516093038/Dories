using YooAsset;

namespace Dories.Runtime.YooAssetResourceSystem.Operations.CreateDownloaderOperation
{
    public interface IYooAssetCreateDownloaderOperation
    {
        ResourceDownloaderOperation CreateDownloader(ResourcePackage package);
    }
}