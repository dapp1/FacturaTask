using Assets.Scripts.Runtime;
using DIContainer;
using EventBusSystem;
using Factory;
using UnityEngine;
using static EventBusSystem.Events;

public class TurretController : MonoBehaviour
{
    [SerializeField] private LineRenderer _line;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private float _sensitivity = 0.5f;
    [SerializeField] private float _reloadTime = 1f;
    [SerializeField] private LayerMask _enemyLayer;

    private float _nextShootTime;
    private float _touchStartPosX;
    private float _initialRotationY;
    private Vector2 _rotationBounds = new(-45, 45);

    [Inject] private IObjectFactory _objectFactory;

    private void Awake()
    {
        _objectFactory.InitializePool(_projectilePrefab);
        EventBus.Subscribe<OnGameStartedEvent>(OnLevelStarted);
    }

    private void Update()
    {
        CheckRotation();
        TryFindEnemyAndShoot();
    }

    [ContextMenu("Pew pew pew")]
    private void Shoot()
    {
        var projectile = _objectFactory.Create(_projectilePrefab, _line.transform.position);
        projectile.Launch(transform.forward);
    }

    private void CheckRotation()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _touchStartPosX = Input.mousePosition.x;
            _initialRotationY = transform.localEulerAngles.y;
            if (_initialRotationY > 180) _initialRotationY -= 360;
        }

        if (Input.GetMouseButton(0))
        {
            float deltaX = _touchStartPosX - Input.mousePosition.x;
            float newAngleY = _initialRotationY + (deltaX * _sensitivity);
            newAngleY = Mathf.Clamp(newAngleY, _rotationBounds.x, _rotationBounds.y);

            transform.localRotation = Quaternion.Euler(0, newAngleY, 0);
        }
    }

    private void TryFindEnemyAndShoot()
    {
        if (Time.time < _nextShootTime) return;

        Ray ray = new Ray(_line.transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 15, _enemyLayer))
        {
            Debug.Log(hit.collider);

            var projectile = _objectFactory.Create(_projectilePrefab, _line.transform.position);
            projectile.Launch(transform.forward);

            _nextShootTime = Time.time + _reloadTime;
        }
    }

    void OnLevelStarted(OnGameStartedEvent busEvent)
    {
        _line.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<OnGameStartedEvent>(OnLevelStarted);
    }
}
