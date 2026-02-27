using YooAsset;

namespace Dories.Runtime.YooAssetResourceSystem.Operations.InitPackageOperation
{
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