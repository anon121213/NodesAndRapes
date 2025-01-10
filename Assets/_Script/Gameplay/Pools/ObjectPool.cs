using System.Collections.Generic;
using UnityEngine;

namespace _Script.Gameplay.Pools
{
    public abstract class ObjectPool<T> where T : Component
    {
        private readonly Queue<T> _pool = new ();
        private readonly T _prefab;
        private readonly Transform _parent;

        protected ObjectPool(T prefab, Transform parent = null, int initialSize = 0)
        {
            _prefab = prefab;
            _parent = parent;

            for (int i = 0; i < initialSize; i++)
            {
                T obj = CreateObject();
                ReturnObject(obj);
            }
        }

        public T GetObject()
        {
            if (_pool.Count <= 0) 
                return CreateObject();
            
            T obj = _pool.Dequeue();
            obj.gameObject.SetActive(true);
            OnObjectTaken(obj);
            
            return obj;
        }

        public void ReturnObject(T obj)
        {
            if (obj == null) return;

            obj.gameObject.SetActive(false);
            _pool.Enqueue(obj);
            OnObjectReturned(obj);
        }

        public void ClearPool()
        {
            while (_pool.Count > 0)
            {
                T obj = _pool.Dequeue();
                Object.Destroy(obj.gameObject);
            }
        }

        private T CreateObject()
        {
            T obj = Object.Instantiate(_prefab, _parent);
            obj.gameObject.SetActive(false);
            OnObjectCreated(obj);
            return obj;
        }

        protected virtual void OnObjectCreated(T obj) { }

        protected virtual void OnObjectTaken(T obj) { }

        protected virtual void OnObjectReturned(T obj) { }
    }
}
