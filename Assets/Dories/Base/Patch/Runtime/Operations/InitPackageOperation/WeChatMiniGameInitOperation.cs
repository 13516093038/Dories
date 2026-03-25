using YooAsset;

namespace Dories.Base.Patch.Runtime.Operations.InitPackageOperation
{
    public class WeChatMiniGameInitOperation : IYooAssetInitOperation
    {
        public InitializationOperation Init(ResourcePackage package, string packageName, IRemoteServices remoteServices)
        {
            var createParameters = new WebPlayModeParameters();
#if UNITY_WEBGL && WEIXINMINIGAME && !UNITY_EDITOR
            string packageRoot = $"{WeChatWASM.WX.env.USER_DATA_PATH}/__GAME_FILE_CACHE"; //注意：如果有子目录，请修改此处！
            IRemoteServices remoteServices = new RemoteServices(defaultHostServer, fallbackHostServer);
            createParameters.WebServerFileSystemParameters =
            WechatFileSystemCreater.CreateFileSystemParameters(packageRoot, remoteServices);
#endif
            return package.InitializeAsync(createParameters);
        }
    }
}