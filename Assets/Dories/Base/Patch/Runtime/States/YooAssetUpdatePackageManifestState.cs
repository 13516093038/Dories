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