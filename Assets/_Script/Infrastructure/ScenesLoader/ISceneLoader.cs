using System;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace _Script.Infrastructure.ScenesLoader
{
    public interface ISceneLoader
    {
        UniTask LoadScene(AssetReference nextScene, Action onLoaded = null);
    }
}