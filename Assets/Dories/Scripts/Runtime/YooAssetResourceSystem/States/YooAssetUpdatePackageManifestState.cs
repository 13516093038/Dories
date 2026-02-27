using Cysharp.Threading.Tasks;
using Dories.FsmSystem.Runtime.Fsm;
using Dories.Runtime.YooAssetResourceSystem;
using Dories.Runtime.YooAssetResourceSystem.States;
using YooAsset;

namespace YooAssetResourceSystem.Runtime.States
{
    public class YooAssetUpdatePackageManifestState : StateBase<YooAssetSystem>
    {
        public override void OnEnter()
        {
            base.OnEnter();

            UpdatePackageManifestTask(Owner.m_Package, Owner.m_PackageVersion).Forget();
        }

        private async UniTask UpdatePackageManifestTask(ResourcePackage package, string packageVersion)
        {
            var operation = Owner.m_UpdatePackageManifestOperation.UpdatePackageManifest(package, packageVersion);
            await operation;
            ChangeState<YooAssetCreateDownloaderState>();
        }
    }
}