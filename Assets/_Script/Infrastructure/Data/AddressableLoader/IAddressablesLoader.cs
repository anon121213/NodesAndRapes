using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace _Script.Infrastructure.Data.AddressableLoader
{
    public interface IAddressablesLoader
    {
        UniTask InitializeAsset();
        UniTask<T> LoadAsync<T>(string address) where T : class;
        UniTask<T> LoadAsync<T>(AssetReference assetReference) where T : class;
        UniTask<List<T>> LoadAssetsByLabelAsync<T>(string label) where T : class;
        void Cleanup();
    }
}