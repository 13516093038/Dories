using System;
using System.Collections.Generic;
using Dories.Base.Fsm.Runtime;
using Dories.Base.Componentization.Runtime;
using Dories.Base.Componentization.Runtime.Utils;
using Dories.Base.Patch.Runtime.Operations.ClearCacheBundleOperation;
using Dories.Base.Patch.Runtime.Operations.CreateDownloaderOperation;
using Dories.Base.Patch.Runtime.Operations.InitPackageOperation;
using Dories.Base.Patch.Runtime.Operations.RequestPackageVersionOperation;
using Dories.Base.Patch.Runtime.Operations.UpdatePackageManifestOperation;
using Dories.Base.Patch.Runtime.Operations.DownloadFileOverOperation;
using Dories.Base.Patch.Runtime.States;
using YooAsset;

namespace Dories.Base.Patch.Runtime
{
    public class PatchEntity : Entity, IFsmOwner
    {
        private FsmSystem m_FsmSystem;

        internal IYooAssetInitOperation m_InitOperation;
        internal IYooAssetRequestPackageVersionOperation m_RequestPackageVersionOperation;
        internal IYooAssetUpdatePackageManifestOperation m_UpdatePackageManifestOperation;
        internal IYooAssetCreateDownloaderOperation m_CreateDownloaderOperation;
        internal IYooAssetsDownloadFileOverOperation m_DownloadFileOverOperation;
        internal IYooAssetClearCacheBundleOperation m_ClearCacheBundleOperation;

        internal ResourcePackage m_Package;
        internal string m_PackageName;
        internal string m_PackageVersion;

        internal ResourceDownloaderOperation m_Downloader;
        
        protected internal override void OnAcquire(object userData = null)
        {
            m_FsmSystem = ComponentFactory.Acquire<FsmSystem>();
            m_FsmSystem.CreateFsm(this, new List<Type>
            {
                typeof(YooAssetInitState),
                typeof(YooAssetRequestPackageVersionState),
                typeof(YooAssetUpdatePackageManifestState),
                typeof(YooAssetCreateDownloaderState),
                typeof(YooAssetDownloadPackageFilesState),
                typeof(YooAssetDownloadFileOverState),
                typeof(YooAssetClearCacheBundleState),
            });
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
            m_UpdatePackageManifestOperation = updatePackageManifestOperation;
        }

        public void SetYooAssetCreateDownloaderOperation(IYooAssetCreateDownloaderOperation createDownloaderOperation)
        {
            m_CreateDownloaderOperation = createDownloaderOperation;
        }

        public void SetYooAssetDownloadFileOverOperation(IYooAssetsDownloadFileOverOperation downloadFileOverOperation)
        {
            m_DownloadFileOverOperation = downloadFileOverOperation;
        }

        public void SetYooAssetClearCacheBundleOperation(IYooAssetClearCacheBundleOperation clearCacheBundleOperation)
        {
            m_ClearCacheBundleOperation = clearCacheBundleOperation;
        }

        public void StartPatch()
        {
            m_FsmSystem.StartFsm(this, typeof(YooAssetInitState));
        }
    }
}