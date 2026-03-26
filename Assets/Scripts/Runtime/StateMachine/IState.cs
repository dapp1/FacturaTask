using System;

namespace NewStateMachine
{
    public interface IState<TData> : IState where TData : StateDataBase
    {
        event Action<TData> RequestToTransition;
        void OnEnter(TData data);
    }
    public interface IState
    {
        void FixedUpdate();
        void OnExit();
    }
}