using _Script.Gameplay.Nodes;
using _Script.Gameplay.Pools;
using _Script.Infrastructure.Data.AddressableLoader;
using _Script.Infrastructure.Data.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Script.Infrastructure.Factories
{
    public class NodeFactory : INodeFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataProvider _staticDataProvider;
        
        private GameObject _nodePrefab;
        private NodePool _nodePool;

        public NodeFactory(IAssetProvider assetProvider,
            IStaticDataProvider staticDataProvider)
        {
            _assetProvider = assetProvider;
            _staticDataProvider = staticDataProvider;
        }
        
        public async UniTask Initialize()
        {
            _nodePrefab = await _assetProvider.LoadAsync<GameObject>
                (_staticDataProvider.AssetsReferences.NodeReference);
            
            _nodePool = new NodePool(_nodePrefab.GetComponent<Node>(), null,
                _staticDataProvider.NodesGeneratorConfig.NodeCount);
        }
        
        public Node GetNode(float maxX, float maxY, float minX, float minY)
        {
            Node node = _nodePool.GetObject();
            
            node.transform.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
            node.gameObject.SetActive(true);
            
            return node;
        }

        public void ReturnToPool(Node node) => 
            _nodePool.ReturnObject(node);
    }
}