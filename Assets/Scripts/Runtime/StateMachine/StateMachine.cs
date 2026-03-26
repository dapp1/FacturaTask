using System.Collections.Generic;

namespace NewStateMachine
{
    public enum StateType
    {
        Idle, Run, Attack, GetDamage, Fly, Death
    }
    
    public class StateMachine
    {
        private IReadOnlyDictionary<StateType, IState<StateDataBase>> _unitStates;

        private IState<StateDataBase> _currentState;
        public IState CurrentState => _currentState;

        public StateMachine(Dictionary<StateType, IState<StateDataBase>> states)
        {
            _unitStates = states;
            
            foreach (var state in _unitStates.Values)
            {
                state.RequestToTransition += OnRequestTransition;
            }

            ChangeState(new StateDataBase(StateType.Idle));
        }

        public void StopStateMachine()
        {
            _currentState?.OnExit();
            _currentState = null;
        }

        public void FixedUpdate()
        {
            _currentState?.FixedUpdate();
        }

        private void OnRequestTransition(StateDataBase data)
        {
            _currentState?.OnExit();
            _currentState = _unitStates[data.StateType];
            _currentState.OnEnter(data);
        }

        
        public void ChangeState(StateDataBase data)
        {
            _currentState?.OnExit();
            _currentState = _unitStates[data.StateType];
            _currentState.OnEnter(data);
        }
    }
}
