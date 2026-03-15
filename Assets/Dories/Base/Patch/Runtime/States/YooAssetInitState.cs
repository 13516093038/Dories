using Cysharp.Threading.Tasks;
using Dories.Base.Fsm.Runtime;
using YooAsset;

namespace Dories.Base.Patch.Runtime.States
{
    public class YooAssetInitState : StateBase<PatchEntity>
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