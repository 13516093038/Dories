using Dories.Base.Fsm.Runtime;
using YooAsset;

namespace Dories.Base.Patch.Runtime.States
{
    public class YooAssetCreateDownloaderState : StateBase<PatchEntity>
    {
        public override void OnEnter()
        {
            base.OnEnter();

            CreateDownloaderTask(Owner.m_Package);
        }

        private void CreateDownloaderTask(ResourcePackage package)
        {
            ResourceDownloaderOperation operation = Owner.m_CreateDownloaderOperation.CreateDownloader(package);

            Owner.m_Downloader = operation;

            if (operation.TotalDownloadCount == 0)
            {
                //无需下载
                ChangeState<YooAssetDownloadFileOverState>();
            }
            else
            {
                var onNeedUpdateCallback = Owner.m_CreateDownloaderOperation.GetOnNeedUpdateCallback();
                onNeedUpdateCallback?.Invoke(operation.TotalDownloadCount, () =>
                {
                    //抓换到下载资源状态
                    ChangeState<YooAssetDownloadPackageFilesState>();
                });
            }
        }
    }
}