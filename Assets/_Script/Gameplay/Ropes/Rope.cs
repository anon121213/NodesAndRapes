using _Script.Gameplay.Nodes;
using _Script.Infrastructure.Data.StaticData;
using UnityEngine;

namespace _Script.Gameplay.Ropes
{
    public class Rope : MonoBehaviour
    {
        private LineRenderer _lineRenderer;

        private Material _greenMaterial;
        private Material _redMaterial;
        
        public Node StartNode { get; private set; }
        public Node EndNode { get; private set; }

        public void Initialize(Node startNode,
            Node endNode,
            Material greenMat,
            Material redMat)
        {
            StartNode = startNode;
            EndNode = endNode;
            _greenMaterial = greenMat;
            _redMaterial = redMat;
        }
        
        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.positionCount = 2;
        }

        public void UpdateRope()
        {
            _lineRenderer.SetPosition(0, StartNode.transform.position);
            _lineRenderer.SetPosition(1, EndNode.transform.position);
        }

        public void SetIntersection(bool isIntersection) => 
            _lineRenderer.material = isIntersection ? _redMaterial : _greenMaterial;
    }
}