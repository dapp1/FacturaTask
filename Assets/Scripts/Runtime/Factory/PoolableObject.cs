using Factory;
using UnityEngine;

public class PoolableObject : MonoBehaviour
{
    private ObjectPool _pool;

    public void Init(ObjectPool pool)
    {
        _pool = pool;
    }

    private void OnDisable()
    {
        if (_pool != null)
        {
            _pool.ReturnToPool(gameObject);
        }
    }
}
