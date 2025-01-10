using System.Collections.Generic;
using System.Linq;
using _Script.Gameplay.Nodes;
using _Script.Gameplay.Pools;
using _Script.Gameplay.Ropes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Script.Gameplay
{
    public class NodeManager : MonoBehaviour
    {
        private readonly HashSet<(Node, Node)> connectedPairs = new();
        private readonly List<Rope> _allRopes = new();
        private readonly List<Node> _allNodes = new();

        private NodePool _nodePool;
        private RopePool _ropePool;

        public GameObject nodePrefab;
        public GameObject ropePrefab;

        public int nodeCount = 5;
        public float minX = -5f, maxX = 5f, minY = -5f, maxY = 5f;

        private void Start()
        {
            _nodePool = new NodePool(nodePrefab.GetComponent<Node>(), transform, nodeCount);
            _ropePool = new RopePool(ropePrefab.GetComponent<Rope>(), transform, nodeCount * 2);

            GenerateRandomNodesAndRopes();
        }

        private void Update()
        {
            CheckForRopeIntersections(_allRopes);
        }

        public void GenerateRandomNodesAndRopes()
        {
            // Возвращаем старые узлы и веревки в пул
            foreach (var node in _allNodes)
            {
                _nodePool.ReturnObject(node);
            }
            _allNodes.Clear();

            foreach (var rope in _allRopes)
            {
                _ropePool.ReturnObject(rope);
            }
            _allRopes.Clear();
            connectedPairs.Clear();

            // Генерируем новые узлы
            for (int i = 0; i < nodeCount; i++)
            {
                Node node = _nodePool.GetObject();
                node.transform.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
                node.gameObject.SetActive(true);
                _allNodes.Add(node);
            }

            // Генерация соединений
            foreach (Node node in _allNodes)
            {
                int ropeCount = 2; // Минимум 2 соединения
                for (int i = 0; i < ropeCount; i++)
                {
                    Node otherNode = GetRandomNodeExcluding(node, _allNodes);
                    if (otherNode != null && !IsAlreadyConnected(node, otherNode))
                    {
                        Rope rope = _ropePool.GetObject();
                        rope.Initialize(node.transform, otherNode.transform);

                        // Добавляем веревки в массив узлов
                        node.AddRope(rope);
                        otherNode.AddRope(rope);

                        rope.UpdateRope();

                        // Добавляем пару соединенных узлов в HashSet
                        connectedPairs.Add((node, otherNode));
                        connectedPairs.Add((otherNode, node));

                        _allRopes.Add(rope);
                    }
                }
            }

            // Проверка пересечений веревок и изменение их цвета
            CheckForRopeIntersections(_allRopes);
        }

        // Проверка пересечений всех веревок
        private void CheckForRopeIntersections(List<Rope> allRopes)
        {
            foreach (var rope1 in allRopes)
            {
                foreach (var rope2 in allRopes)
                {
                    if (rope1 == rope2) continue;

                    if (RopeUtils.AreSegmentsIntersecting(rope1.StartNode.position, rope1.EndNode.position,
                        rope2.StartNode.position, rope2.EndNode.position))
                    {
                        rope1.GetComponent<LineRenderer>().startColor = Color.red;
                        rope1.GetComponent<LineRenderer>().endColor = Color.red;
                        rope2.GetComponent<LineRenderer>().startColor = Color.red;
                        rope2.GetComponent<LineRenderer>().endColor = Color.red;
                    }
                }
            }
        }

        // Получаем случайный узел, исключая данный узел
        private Node GetRandomNodeExcluding(Node excludeNode, List<Node> nodes)
        {
            Node randomNode;
            do
            {
                randomNode = nodes[Random.Range(0, nodes.Count)];
            } while (randomNode == excludeNode);

            return randomNode;
        }

        // Проверка, были ли узлы уже соединены
        private bool IsAlreadyConnected(Node node1, Node node2)
        {
            return connectedPairs.Contains((node1, node2)) || connectedPairs.Contains((node2, node1));
        }
    }
}