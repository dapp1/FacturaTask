using System.Collections.Generic;
using DIContainer;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Factory
{ 
    public class EnemyFactory : IEnemyFactory
    {
        // private EntitiesFactoryConfig _config;
        //private Dictionary<EntityType, ObjectPool> _pools = new Dictionary<EntityType, LocalEntityPool>();
        private Container _container;
        
        [Inject]
        public GameObject CreateEntity()
        {
            throw new System.NotImplementedException();
        }

        //public EntityFactory(EntitiesFactoryConfig config, Container container)
        //{
        //    _config = config;
        //    _container = container;
        //}

        //public void InitializeFactory(EntityType type, int maxCount = -1)
        //{
        //    if (!_pools.TryGetValue(type, out var pool))
        //    {
        //        var prefab = _config.GetPrefabByType(type);
        //        pool = new LocalEntityPool(prefab, _container, maxCount);
        //        _pools[type] = pool;
        //    }
        //}

        public GameObject CreateEntity(EntityType type, Vector2 position, int maxCount = -1)
        {
            if (!_pools.TryGetValue(type, out var pool))
            {
                var prefab = _config.GetPrefabByType(type);
                pool = new LocalEntityPool(prefab, _container, maxCount, null);
                _pools[type] = pool;
            }

            return pool.Get(position);
        }
    }
}