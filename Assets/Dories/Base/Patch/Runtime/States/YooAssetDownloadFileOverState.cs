using Dories.Base.Fsm.Runtime;

namespace Dories.Base.Patch.Runtime.States
{
    public class YooAssetDownloadFileOverState : StateBase<PatchEntity>
    {
        public override void OnEnter()
        {
            base.OnEnter();
            Owner.m_DownloadFileOverOperation.DownloadFileOver()?.Invoke();
            ChangeState<YooAssetClearCacheBundleState>();
        }
    }
}