using System.Collections;
using UnityEngine;

namespace NewStateMachine
{
    public class RunStateData : StateDataBase
    {
        public Transform Target { get; private set; }

        public RunStateData(Transform target) : base(StateType.Run)
        {
            Target = target;
        }
    }
}