using _Script.Gameplay.Ropes;
using UnityEngine;
using UnityEngine.Pool;

namespace _Script.Gameplay.Pools
{
    public class RopePool : ObjectPool<Rope>
    {
        private Transform poolParent;

        public RopePool(Rope prefab, Transform parent = null, int initialSize = 0) 
            : base(prefab, parent, initialSize) { }
    }
}