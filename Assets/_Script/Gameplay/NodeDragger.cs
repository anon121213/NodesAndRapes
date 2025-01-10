using _Script.Gameplay.Nodes;
using UnityEngine;

namespace _Script.Gameplay
{
    public class NodeDragger : MonoBehaviour
    {
        private Camera mainCamera;
        private Node selectedNode;
        private Vector2 offset;  // Смещение между курсором и центром узла

        private void Start()
        {
            mainCamera = Camera.main;
        }

        private void Update()
        {
            HandleDrag();
        }

        private void HandleDrag()
        {
            // При нажатии на мышь
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                Collider2D hit = Physics2D.OverlapPoint(mousePosition);
                if (hit != null && hit.GetComponent<Node>() != null)
                {
                    selectedNode = hit.GetComponent<Node>();
                    // Вычисляем смещение между узлом и позицией мыши
                    offset = (Vector2)selectedNode.transform.position - mousePosition;
                }
            }

            // Пока мышь зажата и выбран узел
            if (Input.GetMouseButton(0) && selectedNode != null)
            {
                Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                // Перемещаем узел с учетом смещения
                selectedNode.transform.position = mousePosition + offset;
            }

            // Когда отпускаем кнопку мыши
            if (Input.GetMouseButtonUp(0))
            {
                selectedNode = null;
            }
        }
    }
}