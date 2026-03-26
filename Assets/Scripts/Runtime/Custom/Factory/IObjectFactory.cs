using UnityEngine;

namespace Factory
{
    public interface IObjectFactory
    {
        void InitializePool<T>(T prefab, int size = -1) where T : MonoBehaviour;
        void InitializePool(GameObject prefab, int size = -1);
        GameObject Create(GameObject prefab, Vector3 pos);
        public T Create<T>(T prefab, Vector3 pos) where T : MonoBehaviour;
    }
}