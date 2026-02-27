using Dories.FsmSystem.Runtime.Fsm;
using Dories.Runtime.Componentization.Utils;
using Dories.Runtime.YooAssetResourceSystem.Operations.CreateDownloaderOperation;
using Dories.Runtime.YooAssetResourceSystem.Operations.InitPackageOperation;
using Dories.Runtime.YooAssetResourceSystem.Operations.RequestPackageVersionOperation;
using Dories.Runtime.YooAssetResourceSystem.Operations.UpdatePackageManifestOperation;
using YooAsset;

namespace Dories.Runtime.YooAssetResourceSystem
{
    public class YooAssetSystem : Entity, IFsmOwner
    {
        private FsmSystem.Runtime.Fsm.FsmSystem m_FsmSystem;

        internal IYooAssetInitOperation m_InitOperation;
        internal IYooAssetRequestPackageVersionOperation m_RequestPackageVersionOperation;
        internal IYooAssetUpdatePackageManifestOperation m_UpdatePackageManifestOperation;
        internal IYooAssetCreateDownloaderOperation m_CreateDownloaderOperation;

        internal ResourcePackage m_Package;
        internal string m_PackageName;
        internal string m_PackageVersion;
        
        protected internal override void OnAcquire(object userData = null)
        {
            m_FsmSystem = ComponentFactory.Acquire<FsmSystem.Runtime.Fsm.FsmSystem>();
        }

        public void SetYooAssetInitoperation(IYooAssetInitOperation initOperation)
        {
            m_InitOperation = initOperation;
        }

        public void SetYooAssetRequestPackageVersionOperation(
            IYooAssetRequestPackageVersionOperation requestPackageVersionOperation)
        {
            m_RequestPackageVersionOperation = requestPackageVersionOperation;
        }

        public void SetYooAssetUpdatePackageManifestOperation(
            IYooAssetUpdatePackageManifestOperation updatePackageManifestOperation)
        {
            
        }

        public void SetYooAssetCreateDownloaderOperation(IYooAssetCreateDownloaderOperation createDownloaderOperation)
        {
            m_CreateDownloaderOperation = createDownloaderOperation;
        }
    }
}