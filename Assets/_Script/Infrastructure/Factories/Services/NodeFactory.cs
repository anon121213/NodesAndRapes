using System.Collections.Generic;
using _Script.Gameplay.Nodes;
using _Script.Gameplay.Nodes.Mover;
using _Script.Gameplay.Nodes.NodeUi;
using _Script.Gameplay.Pools;
using _Script.Gameplay.SoundSystem;
using _Script.Gameplay.WinSystem.Checker;
using _Script.Infrastructure.Data.AddressableLoader;
using _Script.Infrastructure.Data.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Script.Infrastructure.Factories.Services
{
    public class NodeFactory : INodeFactory
    {
        private readonly IAddressablesLoader _addressablesLoader;
        private readonly IStaticDataProvider _staticDataProvider;
        private readonly IWineble _winService;
        private readonly ISoundService _soundService;
        private readonly List<Node> _nodes = new ();

        private GameObject _nodePrefab;
        private NodePool _nodePool;

        public NodeFactory(IAddressablesLoader addressablesLoader,
            IStaticDataProvider staticDataProvider,
            IWinService winService,
            ISoundService soundService)
        {
            _addressablesLoader = addressablesLoader;
            _staticDataProvider = staticDataProvider;
            _winService = winService;
            _soundService = soundService;
        }
        
        public async UniTask Initialize()
        {
            _nodePrefab = await _addressablesLoader.LoadAsync<GameObject>
                (_staticDataProvider.AssetsReferences.NodeReference);
            
            _nodePool = new NodePool(_nodePrefab.GetComponent<Node>(), null,
                _staticDataProvider.NodesGeneratorConfig.NodeCount);
        }
        
        public Node GetNode(float maxX, float maxY, float minX, float minY)
        {
            Node node = _nodePool.GetObject();

            InitNode(node);
            
            node.transform.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
            node.gameObject.SetActive(true);
            
            return node;
        }

        private void InitNode(Node node)
        {
            if (_nodes.Contains(node)) 
                return;
            
            _nodes.Add(node);
            
            INodeMover nodeMover = new NodeMover(node, _winService);
            INodePresenter nodePresenter = new NodePresenter();
            INodeView nodeView = node.GetComponent<NodeView>();
            
            nodePresenter.Initialize(node, nodeView);
            node.Initialize(nodeMover, nodePresenter, _soundService, _staticDataProvider);
        }

        public void ReturnToPool(Node node) => 
            _nodePool.ReturnObject(node);
    }
}