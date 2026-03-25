using Enemy;
using UnityEngine;

namespace Factory
{
    public interface IEnemyFactory
    {
        void InitializeFactory(EnemyType type, int initialCount);
        GameObject CreateEntity(EnemyType type, Vector3 pos);
    }
}