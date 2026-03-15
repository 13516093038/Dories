using Cysharp.Threading.Tasks;
using Dories.Base.Fsm.Runtime;
using YooAsset;

namespace Dories.Base.Patch.Runtime.States
{
    public class YooAssetClearCacheBundleState : StateBase<PatchEntity>
    {
        public override void OnEnter()
        {
            base.OnEnter();
            ClearCacheBundle().Forget();
        }

        private async UniTaskVoid ClearCacheBundle()
        {
            var operation = Owner.m_ClearCacheBundleOperation.ClearCacheBundle(Owner.m_Package);
            await operation;
            if(operation.Status == EOperationStatus.Succeed)
            {
                //清理成功
            }
            else
            {
                //清理失败
            }
        }
    }
}