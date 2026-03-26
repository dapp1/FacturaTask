using Configs;
using NewStateMachine;
using DIContainer;
using System;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Runtime;

public class StickmanController : MonoBehaviour, IDamagable
{
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerDetector _detector;
    [SerializeField] private SkinnedMeshRenderer _meshRenderer;

    private int _health;
    private StateMachine _stateMachine;

    public event Action OnDie;

    [Inject] private StickmanConfig _config;

    private void Start()
    {
        _health = _config.Health;

        var states = new Dictionary<StateType, IState<StateDataBase>>() 
        { 
            { StateType.Idle, new IdleState(_detector) }, 
            { StateType.Run, new RunState(_animator, transform , _config.Speed) } 
        };

        _stateMachine = new(states);
    }

    private void FixedUpdate()
    {
        _stateMachine.FixedUpdate();
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        _animator.SetTrigger("Hit");

        if (_health <= 0)
        {
            gameObject.SetActive(false);
            OnDie?.Invoke();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player") && collision.collider.TryGetComponent<IDamagable>(out var damagable))
        {
            damagable.TakeDamage(_config.Damage);
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        ResetState();
    }

    private void ResetState()
    {
        _stateMachine.StopStateMachine();
        _health = _config.Health;
    }
}
