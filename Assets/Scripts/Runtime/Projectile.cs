using UnityEngine;

namespace Assets.Scripts.Runtime
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private TrailRenderer _trailRenderer;
        [SerializeField] private float _speed = 20f;
        [SerializeField] private float _timeToDisable = 2f;

        public void Launch(Vector3 direction)
        {
            _rb.linearVelocity = direction.normalized * _speed;
            transform.forward = direction;

            Invoke(nameof(Disable), _timeToDisable);
        }

        private void Disable()
        {
            gameObject.SetActive(false);
            _trailRenderer.Clear();
        }

        private void OnTriggerEnter(Collider other)
        {
            Disable();
        }
    }
}