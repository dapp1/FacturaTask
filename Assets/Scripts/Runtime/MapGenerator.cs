using DIContainer;
using Factory;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameObject _platformPrefab;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _offsetZ = 75f;
    [SerializeField] private int _maxPlatformsOnLevel = 4;
    [SerializeField] private float _distanceToSpawn = 40f;

    private Vector3 _nextSpawnPos = new Vector3(0, 0, 30);
    private Queue<GameObject> _activePlatforms = new();

    [Inject] private IObjectFactory _factory;
        
    private void Start()
    {
        _factory.InitializePool(_platformPrefab, _maxPlatformsOnLevel);

        for (int i = 0; i < _maxPlatformsOnLevel - 1; i++)
        {
            Generate();
        }
    }

    private void Update()
    {
        if (Vector3.Distance(_playerTransform.position, _nextSpawnPos) < _distanceToSpawn)
        {
            Generate();
        }

        TryRecyclePlatfroms();
    }

    private void Generate()
    {
        var platform = _factory.Create(_platformPrefab, _nextSpawnPos);

        _activePlatforms.Enqueue(platform);
        _nextSpawnPos += new Vector3(0, 0, _offsetZ);
    }

    private void TryRecyclePlatfroms()
    {
        if (_activePlatforms.Count == 0) return;

        GameObject oldestPlatform = _activePlatforms.Peek();

        float recycleThreshold = _offsetZ * 1.5f;

        if (_playerTransform.position.z > oldestPlatform.transform.position.z + recycleThreshold)
        {
            _activePlatforms.Dequeue();
            oldestPlatform.SetActive(false);
        }
    }
}
