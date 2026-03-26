using DIContainer;
using System.Collections.Generic;
using UnityEngine;

namespace Factory
{
    public class ObjectPool
    {
        private readonly Transform _parent;
        private readonly GameObject _prefab;
        private readonly Container _container;

        private readonly List<GameObject> _objects;
        private readonly Queue<GameObject> _inactiveObjects = new();

        public ObjectPool(GameObject prefab, Container container, int initialCount, Transform parent = null)
        {
            _prefab = prefab;
            _parent = parent;
            _objects = new List<GameObject>(10);
            _container = container;

            initialCount = initialCount > 0 ? initialCount : 5;
            if (_parent == null)
            {
                GameObject go = new GameObject($"{prefab.name}_Pool");
                _parent = go.transform;
            }

            for (int i = 0; i < initialCount; i++)
            {
                CreateNewInstance();
            }
        }

        private GameObject CreateNewInstance()
        {
            var obj = _container.Instantiate(_prefab, _parent);

            var poolable = obj.GetComponent<PoolableObject>();
            if (poolable == null) poolable = obj.AddComponent<PoolableObject>();

            poolable.Init(this);
            _inactiveObjects.Enqueue(obj);
            return obj;
        }

        public GameObject Get(Vector3 position)
        {
            GameObject obj = null;

            if (_inactiveObjects.Count <= 0)
            {
                CreateNewInstance();
            }

            obj = _inactiveObjects.Dequeue();

            if (obj != null)
            {
                obj.transform.position = position;
                obj.SetActive(true);
            }

            return obj;
        }

        public void ReturnToPool(GameObject obj)
        {
            _inactiveObjects.Enqueue(obj);
        }
    }
}