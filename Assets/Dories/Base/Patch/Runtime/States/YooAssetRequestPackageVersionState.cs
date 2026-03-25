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

            RequestPackageVersionTask().Forget();
        }

        private async UniTask RequestPackageVersionTask()
        {
            foreach (var packageName in Owner.packagesNameList)
            {
                var operation = Owner.m_RequestPackageVersionOperation.RequestPackageVersion(Owner.m_PackageInfoDic[packageName].Package);
                await operation;
                Owner.m_PackageInfoDic[packageName].PackageVersion = operation.PackageVersion;
            }
           
            ChangeState<YooAssetUpdatePackageManifestState>();
        }
    }
}