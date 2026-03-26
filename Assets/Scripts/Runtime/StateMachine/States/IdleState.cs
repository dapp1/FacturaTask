using Assets.Scripts.Runtime;

using System;
using UnityEngine;

namespace NewStateMachine
{
    public class IdleState : IState<StateDataBase>
    {
        public event Action<StateDataBase> RequestToTransition;

        private PlayerDetector _detector;

        public IdleState(PlayerDetector detector)
        {
            _detector = detector;

            _detector.OnTriggerEntered += ChangeState;
        }

        private void ChangeState(GameObject obj)
        {
            RequestToTransition?.Invoke(new RunStateData(obj.transform));
        }

        public void OnEnter(StateDataBase data)
        {

        }

        public void FixedUpdate()
        {

        }

        public void OnExit()
        {

        }
    }
}