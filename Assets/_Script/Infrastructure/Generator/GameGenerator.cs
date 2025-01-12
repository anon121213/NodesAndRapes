using System.Collections.Generic;
using _Script.Gameplay.Nodes;
using _Script.Gameplay.Pools;
using _Script.Gameplay.Ropes;
using _Script.Infrastructure.Data.StaticData;
using _Script.Infrastructure.Factories;
using Random = UnityEngine.Random;

namespace _Script.Infrastructure.Generator
{
    public class GameGenerator : IGameGenerator
    {
        private readonly IStaticDataProvider _staticDataProvider;
        private readonly IRopeFactory _ropeFactory;
        private readonly INodeFactory _nodeFactory;

        private readonly HashSet<(Node, Node)> connectedPairs = new();

        private readonly List<Rope> _allRopes = new();
        private readonly List<Node> _allNodes = new();

        private NodePool _nodePool;

        private Node _nodePrefab;

        private int _nodeCount, _minRopeCount;
        private float _minX, _maxX, _minY, _maxY;
        private int _maxRopeCount;

        public GameGenerator(IStaticDataProvider staticDataProvider,
            IRopeFactory ropeFactory,
            INodeFactory nodeFactory)
        {
            _staticDataProvider = staticDataProvider;
            _ropeFactory = ropeFactory;
            _nodeFactory = nodeFactory;
        }

        public void Initialize()
        {
            _minRopeCount = _staticDataProvider.NodesGeneratorConfig.MinRopeCount;
            _maxRopeCount = _staticDataProvider.NodesGeneratorConfig.MinRopeCount;
            _nodeCount = _staticDataProvider.NodesGeneratorConfig.NodeCount;

            _minX = _staticDataProvider.NodesGeneratorConfig.MinX;
            _minY = _staticDataProvider.NodesGeneratorConfig.MinY;
            _maxX = _staticDataProvider.NodesGeneratorConfig.MaxX;
            _maxY = _staticDataProvider.NodesGeneratorConfig.MaxY;
        }

        public void GenerateRandomNodesAndRopes()
        {
            foreach (var node in _allNodes) 
                _nodeFactory.ReturnToPool(node);

            foreach (var rope in _allRopes) 
                _ropeFactory.ReturnToPool(rope);
            
            _allNodes.Clear();
            _allRopes.Clear();

            connectedPairs.Clear();

            GenerateNodes();
            GeneratePairs();
        }

        private void GenerateNodes()
        {
            for (int i = 0; i < _nodeCount; i++) 
                _allNodes.Add(_nodeFactory.GetNode(_maxX, _maxY, _minX, _minY));
        }

        private void GeneratePairs()
        {
            foreach (Node firstNode in _allNodes)
            {
                int ropeCount = Random.Range(_minRopeCount, _maxRopeCount); 
                
                for (int i = 0; i < ropeCount; i++)
                {
                    Node otherNode = GetRandomNodeExcluding(firstNode, _allNodes);

                    if (otherNode == null || IsAlreadyConnected(firstNode, otherNode))
                        continue;
                    
                    Rope rope = _ropeFactory.GetRope(firstNode, otherNode);

                    rope.IntersectionChecker.InitInterception();
                    
                    firstNode.AddRope(rope);
                    otherNode.AddRope(rope);

                    connectedPairs.Add((firstNode, otherNode));
                    connectedPairs.Add((otherNode, firstNode));

                    _allRopes.Add(rope);
                }
            }
        }

        private Node GetRandomNodeExcluding(Node excludeNode, List<Node> nodes)
        {
            Node randomNode;

            do
            {
                randomNode = nodes[Random.Range(0, nodes.Count)];
            } while (randomNode == excludeNode);

            return randomNode;
        }

        private bool IsAlreadyConnected(Node node1, Node node2) =>
            connectedPairs.Contains((node1, node2)) || connectedPairs.Contains((node2, node1));
    }
}
