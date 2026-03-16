using Cysharp.Threading.Tasks;
using Dories.Base.Fsm.Runtime;
using Dories.Base.Patch.Runtime;
using YooAsset;

namespace Dories.Base.Patch.Runtime.States
{
    public class YooAssetRequestPackageVersionState : StateBase<PatchEntity>
    {
        public override void OnEnter()
        {
            base.OnEnter();

            RequestPackageVersionTask(Owner.m_Package).Forget();
        }

        private async UniTask RequestPackageVersionTask(ResourcePackage package)
        {
            var operation = Owner.m_RequestPackageVersionOperation.RequestPackageVersion(package);
            await operation;
            Owner.m_PackageVersion = operation.PackageVersion;
            ChangeState<YooAssetUpdatePackageManifestState>();
        }
    }
}