using UnityEngine;

namespace Factory
{
    public interface IEnemyFactory
    {
        GameObject CreateEntity();
    }
}