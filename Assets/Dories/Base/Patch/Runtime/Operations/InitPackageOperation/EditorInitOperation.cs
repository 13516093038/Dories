using YooAsset;

namespace Dories.Base.Patch.Runtime.Operations.InitPackageOperation
{
    /// <summary>
    /// 编辑器初始化操作
    /// </summary>
    public class EditorInitOperation : IYooAssetInitOperation
    {
        public InitializationOperation Init(ResourcePackage package, string packageName)
        {
            var buildResult = EditorSimulateModeHelper.SimulateBuild(packageName);
            var packageRoot = buildResult.PackageRootDirectory;
            var createParameters = new EditorSimulateModeParameters();
            createParameters.EditorFileSystemParameters =
                FileSystemParameters.CreateDefaultEditorFileSystemParameters(packageRoot);
            return package.InitializeAsync(createParameters);
        }
    }
}