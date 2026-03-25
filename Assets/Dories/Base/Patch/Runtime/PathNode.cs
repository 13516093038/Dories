using Dories.Base.Componentization.Runtime;
using Dories.Base.Patch.Runtime.Operations.ClearCacheBundleOperation;
using Dories.Base.Patch.Runtime.Operations.CreateDownloaderOperation;
using Dories.Base.Patch.Runtime.Operations.DownloadFileOverOperation;
using Dories.Base.Patch.Runtime.Operations.InitPackageOperation;
using Dories.Base.Patch.Runtime.Operations.RequestPackageVersionOperation;
using Dories.Base.Patch.Runtime.Operations.UpdatePackageManifestOperation;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Dories.Base.Patch.Runtime
{
    public class PathNode : EntityMono
    {
        [Title("PathMode"), SerializeField] private PathMode pathMode;
        [Title("PackageName"), SerializeField] private string packageName;

        private PatchEntity m_PatchEntity;

        private void Awake()
        {
            m_PatchEntity = AddComponent<PatchEntity>();
        }

        public void StartPatch()
        {
            if (pathMode == PathMode.EditorSimulateMode)
            {
                m_PatchEntity.SetYooAssetInitoperation(new EditorInitOperation());
                m_PatchEntity.SetYooAssetRequestPackageVersionOperation(new DefaultRequestPackageVersionOperation());
                m_PatchEntity.SetYooAssetUpdatePackageManifestOperation(new DefaultUpdatePackageManifestOperation());
                m_PatchEntity.SetYooAssetCreateDownloaderOperation(new DefaultCreateDownloaderOperation());
                m_PatchEntity.SetYooAssetDownloadFileOverOperation(new DefaultDownloadFileOverOperation());
                m_PatchEntity.SetYooAssetClearCacheBundleOperation(new DefaultClearCacheBundleOperation());
                m_PatchEntity.StartPatch(packageName);
            }

            m_PatchEntity.StartPatch(packageName);
        }
    }
}