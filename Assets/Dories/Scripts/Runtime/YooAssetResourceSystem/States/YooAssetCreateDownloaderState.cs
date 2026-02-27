using Cysharp.Threading.Tasks;
using Dories.FsmSystem.Runtime.Fsm;
using YooAsset;

namespace Dories.Runtime.YooAssetResourceSystem.States
{
    public class YooAssetCreateDownloaderState : StateBase<YooAssetSystem>
    {
        public override void OnEnter()
        {
            base.OnEnter();

            CreateDownloaderTask(Owner.m_Package).Forget();
        }

        private async UniTask CreateDownloaderTask(ResourcePackage package)
        {
            var operation = Owner.m_CreateDownloaderOperation.CreateDownloader(package);

            if (operation.TotalDownloadCount == 0)
            {
              
            }
            else
            {
                operation.BeginDownload();
                await operation;
                if(operation.Status == EOperationStatus.Succeed)
                {
                   
                }
                else
                {
                    
                }
            }
        }
    }
}