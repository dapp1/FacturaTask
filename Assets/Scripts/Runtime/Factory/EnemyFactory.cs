using DIContainer;
using Enemy;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Factory
{ 
    public class EnemyFactory : IEnemyFactory
    {
        // private EntitiesFactoryConfig _config;
        private Dictionary<EnemyType, ObjectPool> _pools = new Dictionary<EnemyType, ObjectPool>();
        private Container _container;

        public EnemyFactory(Container container)
        {
            _container = container;
        }

        public GameObject CreateEntity(EnemyType type, Vector3 pos)
        {
            var pool = TryGetPool(type);
            return pool.Get(pos);
        }

        public void InitializeFactory(EnemyType type, int initialCount)
        {
            TryGetPool(type, initialCount);
        }

        ObjectPool TryGetPool(EnemyType type, int size = -1)
        {
            if (!_pools.TryGetValue(type, out var pool))
            {
                //GameObject prefab = _config.GetPrefab(type);
                GameObject prefab = null;

                if (prefab == null)
                    throw new Exception($"Prefab for {type} not found in config!");

                pool = new ObjectPool(prefab, _container, size, null);
                _pools[type] = pool;
            }
            return pool;

        }
    }
}