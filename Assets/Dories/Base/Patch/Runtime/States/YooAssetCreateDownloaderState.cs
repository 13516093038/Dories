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
                if (Owner.m_Downloaders == null)
                {
                    Owner.m_Downloaders = new();
                }
                Owner.m_Downloaders.Add(packageName, Owner.m_CreateDownloaderOperation.CreateDownloader(Owner.m_PackageInfoDic[packageName].Package));
            }

            int totalDownloadCount = 0;
            foreach (var packageName in Owner.m_Downloaders)
            {
                totalDownloadCount +=  packageName.Value.TotalDownloadCount;
            }
            
            if ( totalDownloadCount == 0)
            {
                //无需下载
                ChangeState<YooAssetDownloadFileOverState>();
            }
            else
            {
                var onNeedUpdateCallback = Owner.m_CreateDownloaderOperation.GetOnNeedUpdateCallback();
                onNeedUpdateCallback?.Invoke(totalDownloadCount, () =>
                {
                    //转换到下载资源状态
                    ChangeState<YooAssetDownloadPackageFilesState>();
                });
            }
        }
    }
}