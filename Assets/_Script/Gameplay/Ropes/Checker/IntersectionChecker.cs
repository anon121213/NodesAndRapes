using System;
using _Script.Infrastructure.Data.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Script.Gameplay.Ropes.Checker
{
    public class IntersectionChecker : IIntersectionChecker
    {
        private readonly Material _greenMaterial;
        private readonly Material _redMaterial;
        private readonly IStaticDataProvider _staticDataProvider;
        private readonly Rope _rope;

        private float _ropeThickness; 
        private float _colliderPadding;

        private bool _isInterception = false;
        
        private BoxCollider2D _boxCollider;
        private LineRenderer _lineRenderer;

        public event Action<Rope, bool> ChangeIntersection;
        
        public IntersectionChecker(Rope rope,
            Material greenMaterial,
            Material redMat,
            IStaticDataProvider staticDataProvider)
        {
            _rope = rope;
            _greenMaterial = greenMaterial;
            _redMaterial = redMat;
            _staticDataProvider = staticDataProvider;
        }

        public void Initialize()
        {
            _lineRenderer = _rope.GetComponent<LineRenderer>();
            _boxCollider = _rope.GetComponent<BoxCollider2D>();
            _ropeThickness = _staticDataProvider.RopesConfig.RopeThickness;
            _colliderPadding = _staticDataProvider.RopesConfig.ColliderPadding;
        }

        public async void InitInterception()
        {
            await UniTask.Delay(100);
            
            if (!_isInterception) 
                ChangeIntersection?.Invoke(_rope, true);
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
            Debug.Log(isIntersection);
            ChangeIntersection?.Invoke(_rope, !isIntersection);

            _isInterception = isIntersection;
            
            if (isIntersection && _lineRenderer.material != _redMaterial)
                _lineRenderer.material = _redMaterial;
            else if (!isIntersection && _lineRenderer.material != _greenMaterial)
                _lineRenderer.material = _greenMaterial;
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Rope") && !_isInterception)
                SetIntersection(true);
        }

        public void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Rope") && !_isInterception)
                SetIntersection(true);
        }

        public void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Rope") && _isInterception)
                SetIntersection(false);
        }
    }
}