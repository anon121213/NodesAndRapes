using System;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace _Script.Infrastructure.ScenesLoader
{
    public class SceneLoader : ISceneLoader

    {
        public async UniTask LoadScene(AssetReference nextScene, Action onLoaded = null)
        {
            if (!nextScene.IsDone)
                return;

            AsyncOperationHandle<SceneInstance> waitNextScene = nextScene.LoadSceneAsync();

            await waitNextScene;

            if (waitNextScene.IsDone)
                onLoaded?.Invoke();
        }
    }
}