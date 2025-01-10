using System.Collections.Generic;
using _Script.Gameplay.Ropes;
using UnityEngine;

namespace _Script.Gameplay.Nodes
{
    public class Node : MonoBehaviour
    {
        private readonly List<Rope> Ropes = new(); 
        private Vector2 offset;

        public void AddRope(Rope rope)
        {
            if (Ropes.Count > 4)
                return;
            
            Ropes.Add(rope);
        }

        public void Release()
        {
            Ropes.Clear();
        }
        
        private void OnMouseDown() => 
            offset = (Vector2)transform.position
                     - (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

        private void OnMouseDrag()
        {
            Vector2 mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            transform.position = mousePosition;

            foreach (Rope rope in Ropes) 
                rope.UpdateRope();
        }
    }
}