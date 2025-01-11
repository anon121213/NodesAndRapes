using System;
using System.Collections.Generic;
using _Script.Gameplay.Nodes.Mover;
using _Script.Gameplay.Nodes.NodeUi;
using _Script.Gameplay.Ropes;
using UnityEngine;

namespace _Script.Gameplay.Nodes
{
    public class Node : MonoBehaviour
    {
        private readonly List<Rope> Ropes = new();
        
        private INodeMover _nodeMover;
        private Vector2 _offset;
        private INodePresenter _nodePresenter;

        public event Action<bool> OnMouseOver;

        public void Initialize(INodeMover nodeMover,
            INodePresenter nodePresenter)
        {
            _nodeMover = nodeMover;
            _nodePresenter = nodePresenter;
            _nodeMover.OnNodeDrag += UpdateRopes;
        }

        private void UpdateRopes()
        {
            foreach (Rope rope in Ropes) 
                rope.UpdateRope();
        }

        public void AddRope(Rope rope)
        {
            if (Ropes.Count > 4)
                return;
            
            Ropes.Add(rope);
        }

        private void OnMouseDown() => 
            _nodeMover.OnMouseDown();

        private void OnMouseDrag() => 
            _nodeMover.OnMouseDrag();

        private void OnMouseExit() => 
            OnMouseOver?.Invoke(false);

        private void OnMouseEnter() => 
            OnMouseOver?.Invoke(true);

        public void Release() => 
            Ropes.Clear();

        private void OnDestroy()
        {
            _nodeMover.OnNodeDrag -= UpdateRopes;
            _nodePresenter.Dispose();
        }
    }
}