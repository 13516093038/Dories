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

            InitTask().Forget();
        }

        private async UniTask InitTask()
        {
            // 创建资源包裹类
            YooAssets.Initialize();
            foreach (var packageName in Owner.packagesNameList)
            {
                var package = YooAssets.TryGetPackage(packageName);
                if (package == null)
                    package = YooAssets.CreatePackage(packageName);
                var operation = Owner.m_InitOperation.Init(package, packageName, Owner.remoteServices);
                await operation;
                var packageInfo = new PackageInfo();
                packageInfo.Package = package;
                Owner.m_PackageInfoDic.Add(packageName, packageInfo);
            }
            ChangeState<YooAssetRequestPackageVersionState>();
        }
    }
}