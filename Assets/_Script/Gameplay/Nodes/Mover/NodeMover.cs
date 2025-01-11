using System;
using _Script.Gameplay.WinSystem.Checker;
using UnityEngine;

namespace _Script.Gameplay.Nodes.Mover
{
    public class NodeMover : INodeMover
    {
        private readonly Node _node;
        private readonly IWineble _winService;
        private Vector2 _offset;
        private readonly Camera _camera;

        public event Action OnNodeDrag;
        
        public NodeMover(Node node,
            IWineble winService)
        {
            _node = node;
            _winService = winService;
            _camera = Camera.main;
        }

        public void OnMouseDown() => 
            _offset = (Vector2)_node.transform.position
                      - (Vector2)_camera.ScreenToWorldPoint(Input.mousePosition);

        public void OnMouseDrag()
        {
            if (_winService.IsWin)
                return;
            
            Vector2 mousePosition = (Vector2)_camera.ScreenToWorldPoint(Input.mousePosition) + _offset;
            _node.transform.position = mousePosition;

            OnNodeDrag?.Invoke();
        }
    }
}