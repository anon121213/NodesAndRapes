using _Script.Gameplay.Nodes;
using UnityEngine;

namespace _Script.Gameplay.Ropes
{
    [RequireComponent(typeof(LineRenderer))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class Rope : MonoBehaviour
    {
        private LineRenderer _lineRenderer;
        private BoxCollider2D _boxCollider; 

        private Material _greenMaterial;
        private Material _redMaterial;

        public Node StartNode { get; private set; }
        public Node EndNode { get; private set; }

        private float _ropeThickness = 0.03f; 
        private float _colliderPadding = 0.3f; 

        public void Initialize(Node startNode, Node endNode, Material greenMat, Material redMat)
        {
            StartNode = startNode;
            EndNode = endNode;
            _greenMaterial = greenMat;
            _redMaterial = redMat;

            _lineRenderer = GetComponent<LineRenderer>();
            _boxCollider = GetComponent<BoxCollider2D>();
            
            _lineRenderer.positionCount = 2; 
            _lineRenderer.useWorldSpace = true; 
            _boxCollider.isTrigger = true;

            UpdateRope(); 
        }

        public void UpdateRope()
        {
            Vector3 startPos = StartNode.transform.position;
            Vector3 endPos = EndNode.transform.position;

            _lineRenderer.SetPosition(0, startPos);
            _lineRenderer.SetPosition(1, endPos);

            UpdateBoxCollider(startPos, endPos);
        }

        private void UpdateBoxCollider(Vector3 startPos, Vector3 endPos)
        {
            Vector3 direction = (endPos - startPos).normalized;
            Vector3 trimmedStartPos = startPos + direction * _colliderPadding;
            Vector3 trimmedEndPos = endPos - direction * _colliderPadding;

            Vector3 midPoint = (trimmedStartPos + trimmedEndPos) / 2;
            float length = Vector2.Distance(trimmedStartPos, trimmedEndPos);

            _boxCollider.size = new Vector2(length, _ropeThickness);
            _boxCollider.transform.position = midPoint;

            float angle = Mathf.Atan2(trimmedEndPos.y - trimmedStartPos.y, trimmedEndPos.x - trimmedStartPos.x) * Mathf.Rad2Deg;
            _boxCollider.transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        public void SetIntersection(bool isIntersection) =>
            _lineRenderer.material = isIntersection ? _redMaterial : _greenMaterial;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Rope"))
                SetIntersection(true);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Rope"))
                SetIntersection(true);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Rope"))
                SetIntersection(false);
        }
    }
}
