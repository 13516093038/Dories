using Cysharp.Threading.Tasks;
using Dories.Base.Fsm.Runtime;
using YooAsset;

namespace Dories.Base.Patch.Runtime.States
{
    public class YooAssetUpdatePackageManifestState : StateBase<PatchEntity>
    {
        public override void OnEnter()
        {
            base.OnEnter();

            UpdatePackageManifestTask().Forget();
        }

        private async UniTask UpdatePackageManifestTask()
        {
            foreach (var packageName in Owner.packagesNameList)
            {
                var packageInfo = Owner.m_PackageInfoDic[packageName];
                var operation =
                    Owner.m_UpdatePackageManifestOperation.UpdatePackageManifest(packageInfo.Package,
                        packageInfo.PackageVersion);
                await operation;
            }
          
            ChangeState<YooAssetCreateDownloaderState>();
        }
    }
}