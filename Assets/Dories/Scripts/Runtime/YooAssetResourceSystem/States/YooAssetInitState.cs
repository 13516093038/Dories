using Cysharp.Threading.Tasks;
using Dories.FsmSystem.Runtime.Fsm;
using Dories.Runtime.YooAssetResourceSystem;
using YooAsset;

namespace YooAssetResourceSystem.Runtime.States
{
    public class YooAssetInitState : StateBase<YooAssetSystem>
    {
        public override void OnEnter()
        {
            base.OnEnter();

            InitTask(Owner.m_Package, Owner.m_PackageName).Forget();
        }

        private async UniTask InitTask(ResourcePackage package, string packageName)
        {
            var operation = Owner.m_InitOperation.Init(package, packageName);
            await operation;

            ChangeState<YooAssetRequestPackageVersionState>();
        }
    }
}