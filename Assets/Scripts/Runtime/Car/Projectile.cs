using Configs;
using DIContainer;
using UnityEngine;

namespace Player
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private TrailRenderer _trailRenderer;

        [Inject] private ProjectileConfig _config;

        public void Launch(Vector3 direction)
        {
            _rb.linearVelocity = direction.normalized * _config.Speed;
            transform.forward = direction;

            Invoke(nameof(Disable), _config.TimeToDisable);
        }

        private void Disable()
        {
            gameObject.SetActive(false);
            _trailRenderer.Clear();
        }   

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Enemy") && other.TryGetComponent<IDamagable>(out var damagable))
            {
                damagable.TakeDamage(_config.Damage);
                Disable();
            }
        }
    }
}