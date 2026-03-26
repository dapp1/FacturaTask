using EventBusSystem;
using UnityEngine;
using UnityEngine.Events;
using static EventBusSystem.Events;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour, IDamagable
{
    [Header("Forward Speed")]
    [SerializeField] private float _forwardSpeed = 10f;

    [Header("ZigZag Settings")]
    [SerializeField] private float _amplitude = 2f;
    [SerializeField] private float _frequency = 3f;

    public UnityEvent OnDie;

    private Rigidbody _rb;
    private float _startTime;
    private int _health;

    public void TakeDamage(int damage)
    {
        _health -= damage;
        _health = Mathf.Max(0, _health);

        EventBus.Publish(new OnCarTakeDamage(_health));

        if (_health <= 0)
        {
            OnDie?.Invoke();
        }
    }

    private void Awake()
    {
        _health = 100;
        _rb = GetComponent<Rigidbody>();
        _startTime = Time.time;
    }

    private void FixedUpdate()
    {
        Vector3 forwardMove = transform.forward * _forwardSpeed;
        float timeSynced = (Time.time - _startTime) * _frequency;
        float xOffset = Mathf.Sin(timeSynced) * _amplitude;
        _rb.linearVelocity = new Vector3(xOffset, _rb.linearVelocity.y, forwardMove.z);
    }
}