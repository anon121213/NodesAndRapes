using UnityEngine;

namespace _Script.Gameplay.Ropes
{
    public class Rope : MonoBehaviour
    {
        private LineRenderer lineRenderer;
        
        public Transform StartNode { get; private set; }
        public Transform EndNode { get; private set; }

        public void Initialize(Transform startNode,
            Transform endNode)
        {
            StartNode = startNode;
            EndNode = endNode;
        }
        
        private void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.positionCount = 2;
        }

        public void UpdateRope()
        {
            lineRenderer.SetPosition(0, StartNode.position);
            lineRenderer.SetPosition(1, EndNode.position);
        }
    }
}