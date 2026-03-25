using Dories.Base.Fsm.Runtime;

namespace Dories.Base.Patch.Runtime.States
{
    public class YooAssetCreateDownloaderState : StateBase<PatchEntity>
    {
        public override void OnEnter()
        {
            base.OnEnter();
            CreateDownloaderTask();
        }

        private void CreateDownloaderTask()
        {
            foreach (var packageName in Owner.packagesNameList)
            {
                if (Owner.m_Downloader == null)
                {
                    Owner.m_Downloader =
                        Owner.m_CreateDownloaderOperation.CreateDownloader(Owner.m_PackageInfoDic[packageName].Package);
                }
                else
                {
                    Owner.m_Downloader.Combine(
                        Owner.m_CreateDownloaderOperation.CreateDownloader(Owner.m_PackageInfoDic[packageName]
                            .Package));
                }
            }
            
            if ( Owner.m_Downloader.TotalDownloadCount == 0)
            {
                //无需下载
                ChangeState<YooAssetDownloadFileOverState>();
            }
            else
            {
                var onNeedUpdateCallback = Owner.m_CreateDownloaderOperation.GetOnNeedUpdateCallback();
                onNeedUpdateCallback?.Invoke(Owner.m_Downloader.TotalDownloadCount, () =>
                {
                    //转换到下载资源状态
                    ChangeState<YooAssetDownloadPackageFilesState>();
                });
            }
        }
    }
}