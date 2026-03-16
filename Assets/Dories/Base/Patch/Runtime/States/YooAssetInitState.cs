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

            InitTask(Owner.m_PackageName).Forget();
        }

        private async UniTask InitTask(string packageName)
        {
            // 创建资源包裹类
            YooAssets.Initialize();
            Owner.m_Package = YooAssets.TryGetPackage(packageName);
            if (Owner.m_Package == null)
                Owner.m_Package = YooAssets.CreatePackage(packageName);
            var operation = Owner.m_InitOperation.Init(Owner.m_Package, packageName);
            await operation;

            ChangeState<YooAssetRequestPackageVersionState>();
        }
    }
}