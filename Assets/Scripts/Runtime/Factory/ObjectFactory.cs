using DIContainer;
using Factory;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFactory : IObjectFactory
{
    private Dictionary<int, ObjectPool> _pools = new();
    private Container _container;

    public ObjectFactory(Container container)
    {
        _container = container;
    }

    public GameObject Create(GameObject prefab, Vector3 pos)
    {
        var pool = TryGetPool(prefab);
        return pool.Get(pos);
    }

    public T Create<T>(T prefab, Vector3 pos) where T : MonoBehaviour
    {
        GameObject prefabGameObject = prefab.gameObject;
        var pool = TryGetPool(prefabGameObject);
        GameObject instance = pool.Get(pos);
        return instance.GetComponent<T>();
    }

    public void InitializePool<T>(T prefab, int size = -1) where T : MonoBehaviour
    {
        GameObject prefabGameObject = prefab.gameObject;
        int key = prefabGameObject.GetInstanceID();

        Debug.Log("Initialize Pool Key - " + key);

        var pool = new ObjectPool(prefabGameObject, _container, size, null);
        _pools[key] = pool;
    }

    ObjectPool TryGetPool(GameObject prefab, int size = -1)
    {
        int key = prefab.GetInstanceID();
        Debug.Log("Try get pool Key - " + key);
        if (!_pools.TryGetValue(key, out var pool))
        {
            if (prefab == null)
                throw new Exception($"Prefab for {prefab} not found in config!");

            pool = new ObjectPool(prefab, _container, size, null);
            _pools[key] = pool;
        }
        return pool;
    }
}
