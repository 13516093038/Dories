using Cysharp.Threading.Tasks;
using Dories.FsmSystem.Runtime.Fsm;
using Dories.Runtime.YooAssetResourceSystem;
using YooAsset;

namespace YooAssetResourceSystem.Runtime.States
{
    public class YooAssetRequestPackageVersionState : StateBase<YooAssetSystem>
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

            ChangeState<YooAssetUpdatePackageManifestState>();
        }
    }
}