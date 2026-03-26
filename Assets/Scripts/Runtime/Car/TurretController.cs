using Configs;
using DIContainer;
using EventBusSystem;
using Factory;
using UnityEngine;
using static EventBusSystem.Events;

namespace Player
{
    public class TurretController : MonoBehaviour
    {
        [SerializeField] private LineRenderer _line;
        [SerializeField] private LayerMask _enemyLayer;


        private float _nextShootTime;
        private float _touchStartPosX;
        private float _initialRotationY;
        private Vector2 _rotationBounds = new(-45, 45);

        private IObjectFactory _objectFactory;
        private TurretConfig _config;

        [Inject]
        private void Construct(IObjectFactory objectFactory, TurretConfig config)
        {
            _config = config;
            _objectFactory = objectFactory;
        }

        private void Start()
        {
            _objectFactory.InitializePool(_config.ProjectilePrefab);
            EventBus.Subscribe<OnGameStartedEvent>(OnLevelStarted);
        }

        private void Update()
        {
            CheckRotation();
            TryFindEnemyAndShoot();
        }
        #region Rotation
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
                float newAngleY = _initialRotationY + (deltaX * _config.Sensativity);
                newAngleY = Mathf.Clamp(newAngleY, _rotationBounds.x, _rotationBounds.y);

                transform.localRotation = Quaternion.Euler(0, newAngleY, 0);
            }
        }
        #endregion
        #region Shoot
        private void TryFindEnemyAndShoot()
        {
            if (Time.time < _nextShootTime) return;

            Ray ray = new Ray(_line.transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 15, _enemyLayer))
            {
                Debug.Log(hit.collider);
                Shoot();
            }
        }

        private void Shoot()
        {
            var projectile = _objectFactory.Create(_config.ProjectilePrefab, _line.transform.position);
            projectile.Launch(transform.forward);
            _nextShootTime = Time.time + _config.ReloadTime;
        }
        #endregion

        void OnLevelStarted(OnGameStartedEvent busEvent)
        {
            _line.gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<OnGameStartedEvent>(OnLevelStarted);
        }
    }
}