using System;

namespace _Script.Gameplay.Nodes.Mover
{
    public interface INodeMover
    {
        event Action OnNodeDrag;
        void OnMouseDown();
        void OnMouseDrag();
    }
}