using Factory;
using UnityEngine;
using DIContainer;
using System.Collections.Generic;

public class Platform : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private StickmanController _enemyPrefab; //TODO: In future create config with other types of enemies.

    [Inject] private IObjectFactory _factory;

    private void Awake()
    {
        _factory.InitializePool(_enemyPrefab, 15);
    }

    private void OnEnable()
    {
        int randomCount = Random.Range(2, 6); // hardcode :c

        List<Transform> points = new(_spawnPoints);

        for(int i = 0; i < randomCount; i++)
        {
            int randomPos = Random.Range(0, points.Count);
            _factory.Create(_enemyPrefab, points[randomPos].position);
            points.Remove(points[randomPos]);
        }
    }
}
