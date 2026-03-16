using Dories.Base.Componentization.Runtime.Utils;
using Dories.Base.Patch.Runtime;
using Dories.Base.Patch.Runtime.Operations.ClearCacheBundleOperation;
using Dories.Base.Patch.Runtime.Operations.CreateDownloaderOperation;
using Dories.Base.Patch.Runtime.Operations.DownloadFileOverOperation;
using Dories.Base.Patch.Runtime.Operations.InitPackageOperation;
using Dories.Base.Patch.Runtime.Operations.RequestPackageVersionOperation;
using Dories.Base.Patch.Runtime.Operations.UpdatePackageManifestOperation;
using UnityEngine;
using YooAsset;

namespace Dories.Base
{
    public class GameEntry : MonoBehaviour
    {
        [SerializeField] private EPlayMode m_PlayMode;

        private void Awake()
        {
            
        }

        private void Start()
        {
            var PatchEntity = ComponentFactory.Acquire<PatchEntity>();

            if (m_PlayMode == EPlayMode.EditorSimulateMode)
            {
                PatchEntity.SetYooAssetInitoperation(new EditorInitOperation());
                PatchEntity.SetYooAssetRequestPackageVersionOperation(new DefaultRequestPackageVersionOperation());
                PatchEntity.SetYooAssetUpdatePackageManifestOperation(new DefaultUpdatePackageManifestOperation());
                PatchEntity.SetYooAssetCreateDownloaderOperation(new DefaultCreateDownloaderOperation());
                PatchEntity.SetYooAssetDownloadFileOverOperation(new DefaultDownloadFileOverOperation());
                PatchEntity.SetYooAssetClearCacheBundleOperation(new DefaultClearCacheBundleOperation());
                PatchEntity.StartPatch("DefaultPackage");
            }

        }
    }
}