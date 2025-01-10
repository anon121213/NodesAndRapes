using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Script.Infrastructure.Data.StaticData
{
    public class StaticDataProvider : IStaticDataProvider
    {
        public AssetsReferences AssetsReferences { get; }
        
        public StaticDataProvider(AllData allData) => 
            AssetsReferences = allData.AssetsReferences;
    }
    
    [CreateAssetMenu(fileName = "AssetsReferences", menuName = "Data/AssetsReferences")]
    public class AssetsReferences : ScriptableObject
    {
        [field: SerializeField] public AssetReference MainSceneReference { get; private set; }
        
        [field: SerializeField] public AssetReferenceGameObject NodeReference { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject RopeReference { get; private set; }
    }

    [CreateAssetMenu(fileName = "AllData", menuName = "Data/AllData")]
    public class AllData : ScriptableObject
    {
        [field: SerializeField] public AssetsReferences AssetsReferences { get; private set; }
    }
}