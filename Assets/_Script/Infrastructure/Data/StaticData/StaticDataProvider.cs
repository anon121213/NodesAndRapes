using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

namespace _Script.Infrastructure.Data.StaticData
{
    public class StaticDataProvider : IStaticDataProvider
    {
        public AssetsReferences AssetsReferences { get; }
        public NodesGeneratorConfig NodesGeneratorConfig { get; }
        public RopesConfig RopesConfig { get; }
        public ScoresConfig ScoresConfig { get; }
        
        public StaticDataProvider(AllData allData)
        {
            AssetsReferences = allData.AssetsReferences;
            NodesGeneratorConfig = allData.NodesGeneratorConfig;
            RopesConfig = allData.RopesConfig;
            ScoresConfig = allData.ScoresConfig;
        }
    }
    
    [CreateAssetMenu(fileName = "AssetsReferences", menuName = "Data/AssetsReferences/AssetsReferences")]
    public class AssetsReferences : ScriptableObject
    {
        [field: SerializeField] public AssetReference MainSceneReference { get; private set; }
        
        [field: SerializeField] public AssetReferenceGameObject NodeReference { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject RopeReference { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject WinWindowReference { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject ScoreReference { get; private set; }
    }
    
    [CreateAssetMenu(fileName = "NodesGeneratorConfig", menuName = "Data/Configs/NodesGeneratorConfig")]
    public class NodesGeneratorConfig : ScriptableObject
    {
        [field: SerializeField] public int NodeCount { get; private set; } = 5;
        [field: SerializeField] public int MinRopeCount { get; private set; } = 5;
        
        [field: SerializeField] public float MinX { get; private set; } = -5f;
        [field: SerializeField] public float MaxX { get; private set; } = 5f;
        [field: SerializeField] public float MinY { get; private set; } = -5f;
        [field: SerializeField] public float MaxY { get; private set; } = 5f;
    }
    
    [CreateAssetMenu(fileName = "RopesConfig", menuName = "Data/Configs/RopesConfig")]
    public class RopesConfig : ScriptableObject
    {
        [field: SerializeField] public AssetReference GreenMaterial { get; private set; }
        [field: SerializeField] public AssetReference RegMaterial { get; private set; }
        
        [field: SerializeField] public float RopeThickness { get; private set; } = 0.03f; 
        [field: SerializeField] public float ColliderPadding { get; private set; } = 0.3f; 
    } 
    
    [CreateAssetMenu(fileName = "ScoresConfig", menuName = "Data/Configs/ScoresConfig")]
    public class ScoresConfig : ScriptableObject
    {
        [field: SerializeField] public int WinScoreValue { get; private set; } = 350; 
    }

    [CreateAssetMenu(fileName = "AllData", menuName = "Data/AllData")]
    public class AllData : ScriptableObject
    {
        [field: SerializeField] public AssetsReferences AssetsReferences { get; private set; }
        [field: SerializeField] public NodesGeneratorConfig NodesGeneratorConfig { get; private set; }
        [field: SerializeField] public RopesConfig RopesConfig { get; private set; }
        [field: SerializeField] public ScoresConfig ScoresConfig { get; private set; }
    }
}