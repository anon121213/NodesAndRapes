using _Script.Gameplay.Nodes;
using UnityEngine;

namespace _Script.Gameplay.Ropes
{
    [RequireComponent(typeof(LineRenderer))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class Rope : MonoBehaviour
    {
        private LineRenderer _lineRenderer;
        private IIntersectionChecker _intersectionChecker;

        private Node _startNode;
        private Node _endNode;

        public void Initialize(Node startNode,
            Node endNode,
            IIntersectionChecker intersectionChecker)
        {
            _startNode = startNode;
            _endNode = endNode;
            _intersectionChecker = intersectionChecker;

            _lineRenderer = GetComponent<LineRenderer>();
            
            _lineRenderer.positionCount = 2; 
            _lineRenderer.useWorldSpace = true; 

            UpdateRope(); 
        }

        public void UpdateRope()
        {
            Vector3 startPos = _startNode.transform.position;
            Vector3 endPos = _endNode.transform.position;

            _lineRenderer.SetPosition(0, startPos);
            _lineRenderer.SetPosition(1, endPos);

            _intersectionChecker.UpdateBoxCollider(startPos, endPos);
        }

        private void OnTriggerEnter2D(Collider2D collision) => 
            _intersectionChecker.OnTriggerEnter2D(collision);

        private void OnTriggerStay2D(Collider2D collision) => 
            _intersectionChecker.OnTriggerStay2D(collision);

        private void OnTriggerExit2D(Collider2D collision) => 
            _intersectionChecker.OnTriggerExit2D(collision);
    }
}
