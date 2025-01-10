using System.Collections.Generic;
using _Script.Gameplay.Nodes;
using UnityEngine;
using UnityEngine.Pool;

namespace _Script.Gameplay.Pools
{
    public class NodePool : ObjectPool<Node>
    {
        private Queue<Node> pool;
        private GameObject nodePrefab;
        private Transform poolParent;

        public NodePool(Node prefab, Transform parent = null, int initialSize = 0) 
            : base(prefab, parent, initialSize) { }

        protected override void OnObjectReturned(Node obj)
        {
            obj.Release();
            base.OnObjectReturned(obj);
        }
    }
}