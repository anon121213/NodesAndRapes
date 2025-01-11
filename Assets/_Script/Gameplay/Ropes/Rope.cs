using _Script.Gameplay.Nodes;
using _Script.Gameplay.Ropes.Checker;
using UnityEngine;

namespace _Script.Gameplay.Ropes
{
    [RequireComponent(typeof(LineRenderer))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class Rope : MonoBehaviour
    {
        public IIntersectionChecker IntersectionChecker { get; private set; }
        
        private LineRenderer _lineRenderer;

        private Node _startNode;
        private Node _endNode;

        public void Initialize(Node startNode,
            Node endNode)
        {
            _startNode = startNode;
            _endNode = endNode;
            
            _lineRenderer = GetComponent<LineRenderer>();
            
            _lineRenderer.positionCount = 2; 
            _lineRenderer.useWorldSpace = true; 

            UpdateRope(); 
        }

        public void SetChecker(IIntersectionChecker intersectionChecker)
        {
            IntersectionChecker = intersectionChecker;
            IntersectionChecker.Initialize();
        }

        public void UpdateRope()
        {
            Vector3 startPos = _startNode.transform.position;
            Vector3 endPos = _endNode.transform.position;

            _lineRenderer.SetPosition(0, startPos);
            _lineRenderer.SetPosition(1, endPos);

            IntersectionChecker.UpdateBoxCollider(startPos, endPos);
        }

        private void OnTriggerEnter2D(Collider2D collision) => 
            IntersectionChecker.OnTriggerEnter2D(collision);

        private void OnTriggerStay2D(Collider2D collision) => 
            IntersectionChecker.OnTriggerStay2D(collision);

        private void OnTriggerExit2D(Collider2D collision) => 
            IntersectionChecker.OnTriggerExit2D(collision);
    }
}
