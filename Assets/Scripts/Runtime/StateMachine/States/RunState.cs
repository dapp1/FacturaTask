using System;
using UnityEngine;

namespace NewStateMachine
{
    public class RunState : IState<StateDataBase>
    {
        public event Action<StateDataBase> RequestToTransition;

        private Animator _animator;
        private Transform _target;
        private Transform _transform;
        private float _speed;

        public RunState(Animator animator, Transform transform,float speed)
        {
            _animator = animator;
            _transform = transform;
            _speed = speed;
        }

        public void FixedUpdate()
        {
            if (_target == null)
            {
                RequestToTransition?.Invoke(new StateDataBase(StateType.Idle));
                return;
            }

            Vector3 direction = (_target.position - _transform.position);
            direction.y = 0;

            if (direction.sqrMagnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction.normalized);
                _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, 10 * Time.fixedDeltaTime);
                _transform.Translate(direction.normalized * _speed * Time.fixedDeltaTime, Space.World);
            }
        }

        public void OnEnter(StateDataBase data)
        {
            if(data is RunStateData stateData)
            {
                _target = stateData.Target;
            }
            _animator.SetBool("isRun", true);
        }

        public void OnExit()
        {
            _animator.SetBool("isRun", false);
            _target = null;
        }
    }
}