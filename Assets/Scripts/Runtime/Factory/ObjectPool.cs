using DIContainer;
using System.Collections.Generic;
using UnityEngine;

namespace Factory
{
    public class ObjectPool
    {
        private readonly int _maxCount;
        private readonly Transform _parent;
        private readonly GameObject _prefab;
        private readonly Container _container;

        private readonly List<GameObject> _objects;
        private readonly Queue<GameObject> _inactiveObjects = new();

        public ObjectPool(GameObject prefab, Container container, int maxCount = -1, Transform parent = null)
        {
            _prefab = prefab;
            _parent = parent;
            _maxCount = maxCount;
            _objects = new List<GameObject>(_maxCount <= -1 ? 100 : _maxCount);
            _container = container;

            int initialCount = _maxCount <= -1 ? 5 : _maxCount;

            if (_parent == null)
            {
                GameObject go = new GameObject($"{prefab.name}_Pool");
                _parent = go.transform;
            }

            for (int i = 0; i < initialCount; i++)
            {
                var obj = _container.Instantiate(_prefab, _parent);
                obj.SetActive(false);
                _objects.Add(obj);
            }
        }

        private GameObject CreateNewInstance()
        {
            var obj = _container.Instantiate(_prefab, _parent);
            obj.SetActive(false);
            _objects.Add(obj);
            _inactiveObjects.Enqueue(obj);
            return obj;
        }

        public GameObject Get(Vector2 position)
        {
            GameObject obj = null;

            if (_inactiveObjects.Count > 0)
            {
                obj = _inactiveObjects.Dequeue();
            }
            else if (_maxCount < 0 || _objects.Count < _maxCount)
            {
                CreateNewInstance();
                obj = _inactiveObjects.Dequeue();
            }

            if (obj != null)
            {
                obj.transform.position = position;
                obj.SetActive(true);
            }

            return obj;
        }

        public void ReturnToPool(GameObject obj)
        {
            obj.SetActive(false);
        
            if (!_inactiveObjects.Contains(obj))
                _inactiveObjects.Enqueue(obj);
        }
    }
}