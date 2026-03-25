using System;
using System.Collections.Generic;
using Dories.Base.Fsm.Runtime;
using Dories.Base.Componentization.Runtime.Utils;
using Dories.Base.Patch.Runtime.Operations.ClearCacheBundleOperation;
using Dories.Base.Patch.Runtime.Operations.CreateDownloaderOperation;
using Dories.Base.Patch.Runtime.Operations.InitPackageOperation;
using Dories.Base.Patch.Runtime.Operations.RequestPackageVersionOperation;
using Dories.Base.Patch.Runtime.Operations.UpdatePackageManifestOperation;
using Dories.Base.Patch.Runtime.Operations.DownloadFileOverOperation;
using Dories.Base.Patch.Runtime.RemoteService;
using Dories.Base.Patch.Runtime.States;
using Sirenix.OdinInspector;
using UnityEngine;
using YooAsset;

namespace Dories.Base.Patch.Runtime
{
    public class PatchEntity : MonoBehaviour, IFsmOwner
    {
        [SerializeField] private PlayMode m_PlayMode;
        [SerializeField, Title("AppPackagesName")] internal List<string> packagesNameList; 
        
        //todo:RemoteService
        internal IRemoteServices remoteServices = new DefaultRemoteServices("asd", "ASd");

        internal IYooAssetInitOperation m_InitOperation;
        internal IYooAssetRequestPackageVersionOperation m_RequestPackageVersionOperation;
        internal IYooAssetUpdatePackageManifestOperation m_UpdatePackageManifestOperation;
        internal IYooAssetCreateDownloaderOperation m_CreateDownloaderOperation;
        internal IYooAssetsDownloadFileOverOperation m_DownloadFileOverOperation;
        internal IYooAssetClearCacheBundleOperation m_ClearCacheBundleOperation;
        internal Dictionary<string, PackageInfo> m_PackageInfoDic;
        internal Dictionary<string, ResourceDownloaderOperation> m_Downloaders;
        private FsmSystem m_FsmSystem;
        

        private void Awake()
        {
            m_PackageInfoDic =  new Dictionary<string, PackageInfo>();
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

        public void SetYooAssetInitOperation(IYooAssetInitOperation initOperation)
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
            switch (m_PlayMode)
            {
                case PlayMode.EditorSimulateMode:
                    SetYooAssetInitOperation(new EditorInitOperation());
                    SetYooAssetRequestPackageVersionOperation(new DefaultRequestPackageVersionOperation());
                    SetYooAssetUpdatePackageManifestOperation(new DefaultUpdatePackageManifestOperation());
                    SetYooAssetCreateDownloaderOperation(new DefaultCreateDownloaderOperation());
                    SetYooAssetDownloadFileOverOperation(new DefaultDownloadFileOverOperation());
                    SetYooAssetClearCacheBundleOperation(new DefaultClearCacheBundleOperation());
                    break;
                
                case PlayMode.OfflinePlayMode:
                    SetYooAssetInitOperation(new OfflineInitOperation());
                    SetYooAssetRequestPackageVersionOperation(new DefaultRequestPackageVersionOperation());
                    SetYooAssetUpdatePackageManifestOperation(new DefaultUpdatePackageManifestOperation());
                    SetYooAssetCreateDownloaderOperation(new DefaultCreateDownloaderOperation());
                    SetYooAssetDownloadFileOverOperation(new DefaultDownloadFileOverOperation());
                    SetYooAssetClearCacheBundleOperation(new DefaultClearCacheBundleOperation());
                    break;
                
                case PlayMode.HostPlayMode:
                    SetYooAssetInitOperation(new HostPlayInitOperation());
                    SetYooAssetRequestPackageVersionOperation(new DefaultRequestPackageVersionOperation());
                    SetYooAssetUpdatePackageManifestOperation(new DefaultUpdatePackageManifestOperation());
                    SetYooAssetCreateDownloaderOperation(new DefaultCreateDownloaderOperation());
                    SetYooAssetDownloadFileOverOperation(new DefaultDownloadFileOverOperation());
                    SetYooAssetClearCacheBundleOperation(new DefaultClearCacheBundleOperation());
                    break;
                
                case PlayMode.WeaKOnlinePlayMode:
                    SetYooAssetInitOperation(new WeakOnlineInitOperation());
                    SetYooAssetRequestPackageVersionOperation(new WeakOnlineRequestPackageVersionOperation());
                    SetYooAssetUpdatePackageManifestOperation(new DefaultUpdatePackageManifestOperation());
                    SetYooAssetCreateDownloaderOperation(new DefaultCreateDownloaderOperation());
                    SetYooAssetDownloadFileOverOperation(new DefaultDownloadFileOverOperation());
                    SetYooAssetClearCacheBundleOperation(new DefaultClearCacheBundleOperation());
                    break;
                case PlayMode.WeChatMiniGameMode:
                    break;
            }
            m_FsmSystem.StartFsm(this, typeof(YooAssetInitState));
        }
    }
}