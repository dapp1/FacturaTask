namespace NewStateMachine
{
    public class StateDataBase
    {
        public StateType StateType;

        public StateDataBase(StateType stateType) 
        {
            StateType = stateType;
        }
    }
}