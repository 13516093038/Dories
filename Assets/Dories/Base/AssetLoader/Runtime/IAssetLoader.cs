using Cysharp.Threading.Tasks;

namespace Dories.Base.AssetLoader.Runtime
{
    public interface IAssetLoader
    {
        public delegate void AssetLoadSuccessCallback<T>(T asset) where T : UnityEngine.Object;

        public delegate void AssetLoadFailureCallback(string error);

        public T LoadResource<T>(string path) where T : UnityEngine.Object;

        public UniTask<T> LoadResourceAsync<T>(string path) where T : UnityEngine.Object;

        public UniTaskVoid LoadResourceWithCallback<T>(string path, AssetLoadSuccessCallback<T> successCallback,
            AssetLoadFailureCallback failureCallback) where T : UnityEngine.Object;
    }
}