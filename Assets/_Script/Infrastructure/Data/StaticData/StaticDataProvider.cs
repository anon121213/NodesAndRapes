using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Script.Infrastructure.Data.StaticData
{
    public class StaticDataProvider : IStaticDataProvider
    {
        public AssetsReferences AssetsReferences { get; }
        public NodesGeneratorConfig NodesGeneratorConfig { get; }
        
        public StaticDataProvider(AllData allData)
        {
            AssetsReferences = allData.AssetsReferences;
            NodesGeneratorConfig = allData.NodesGeneratorConfig;
        }
    }
    
    [CreateAssetMenu(fileName = "AssetsReferences", menuName = "Data/AssetsReferences")]
    public class AssetsReferences : ScriptableObject
    {
        [field: SerializeField] public AssetReference MainSceneReference { get; private set; }
        
        [field: SerializeField] public AssetReferenceGameObject NodeReference { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject RopeReference { get; private set; }
    }
    
    [CreateAssetMenu(fileName = "NodesGeneratorConfig", menuName = "Data/Configs")]
    public class NodesGeneratorConfig : ScriptableObject
    {
        [field: SerializeField] public int NodeCount { get; private set; } = 5;
        [field: SerializeField] public int MinRopeCount { get; private set; } = 5;
        
        [field: SerializeField] public float MinX { get; private set; } = -5f;
        [field: SerializeField] public float MaxX { get; private set; } = 5f;
        [field: SerializeField] public float MinY { get; private set; } = -5f;
        [field: SerializeField] public float MaxY { get; private set; } = 5f;
    }

    [CreateAssetMenu(fileName = "AllData", menuName = "Data/AllData")]
    public class AllData : ScriptableObject
    {
        [field: SerializeField] public AssetsReferences AssetsReferences { get; private set; }
        [field: SerializeField] public NodesGeneratorConfig NodesGeneratorConfig { get; private set; }
    }
}