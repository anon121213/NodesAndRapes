using _Script.Infrastructure.Data.StaticData;
using UnityEngine;

namespace _Script.Gameplay
{
    public class IntersectionChecker : IIntersectionChecker
    {
        private readonly BoxCollider2D _boxCollider;
        private readonly LineRenderer _lineRenderer;
        private readonly Material _greenMaterial;
        private readonly Material _redMaterial;
        private readonly IStaticDataProvider _staticDataProvider;

        private float _ropeThickness = 0.03f; 
        private float _colliderPadding = 0.3f; 

        public IntersectionChecker(BoxCollider2D boxCollider,
            LineRenderer lineRenderer,
            Material greenMaterial,
            Material redMat,
            IStaticDataProvider staticDataProvider)
        {
            _boxCollider = boxCollider;
            _lineRenderer = lineRenderer;
            _greenMaterial = greenMaterial;
            _redMaterial = redMat;
            _staticDataProvider = staticDataProvider;
        }

        public void Initialize()
        {
            _ropeThickness = _staticDataProvider.RopesConfig.RopeThickness;
            _colliderPadding = _staticDataProvider.RopesConfig.ColliderPadding;
        }
        
        public void UpdateBoxCollider(Vector3 startPos, Vector3 endPos)
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

        private void SetIntersection(bool isIntersection)
        {
            if (isIntersection && _lineRenderer.material != _redMaterial)
                _lineRenderer.material = _redMaterial;
            else if (!isIntersection && _lineRenderer.material != _greenMaterial) 
                _lineRenderer.material = _greenMaterial;
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Rope"))
                SetIntersection(true);
        }

        public void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Rope"))
                SetIntersection(true);
        }

        public void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Rope"))
                SetIntersection(false);
        }
    }

    public interface IIntersectionChecker
    {
        void Initialize();
        void UpdateBoxCollider(Vector3 startPos, Vector3 endPos);
        void OnTriggerEnter2D(Collider2D collision);
        void OnTriggerStay2D(Collider2D collision);
        void OnTriggerExit2D(Collider2D collision);
    }
}