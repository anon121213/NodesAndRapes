using System;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace _Script.Infrastructure.ScenesLoader
{
    public interface ISceneLoader
    {
        UniTask LoadScene(AssetReference nextScene, Action onLoaded = null);
    }
}