using Cysharp.Threading.Tasks;
using Dories.Base.Fsm.Runtime;
using YooAsset;

namespace Dories.Base.Patch.Runtime.States
{
    /// <summary>
    /// 资源文件下载状态
    /// </summary>
    public class YooAssetDownloadPackageFilesState : StateBase<PatchEntity>
    {
        public override void OnEnter()
        {
            base.OnEnter();
            DownloadPackageFiles().Forget();
        }

        private async UniTaskVoid DownloadPackageFiles()
        {
            Owner.m_Downloader.BeginDownload();
            await Owner.m_Downloader;
            if(Owner.m_Downloader.Status == EOperationStatus.Succeed)
            {
                //下载成功
                ChangeState<YooAssetDownloadFileOverState>();
            }

            //下载失败直接走下载器的错误回调（YooAsset内置）
        }
    }
}