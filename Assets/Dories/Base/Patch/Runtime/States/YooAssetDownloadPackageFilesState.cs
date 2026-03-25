using System.Collections.Generic;
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
            List<UniTask> downloadTasks = new List<UniTask>();
            foreach (var downloader in Owner.m_Downloaders)
            {
                downloader.Value.BeginDownload();
                downloadTasks.Add(downloader.Value.ToUniTask());
            }
            await UniTask.WhenAll(downloadTasks);

            foreach (var downloadTask in Owner.m_Downloaders)
            {
                if (downloadTask.Value.Status == EOperationStatus.Succeed)
                {
                    continue;
                }
                else
                {
                    //下载失败直接走下载器的错误回调（YooAsset内置）
                    return;
                }
            }
            //下载成功
            ChangeState<YooAssetDownloadFileOverState>();
        }
    }
}