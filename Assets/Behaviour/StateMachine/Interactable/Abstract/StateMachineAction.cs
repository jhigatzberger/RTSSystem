using UnityEngine;

namespace JHiga.RTSEngine.StateMachine
{   
    public abstract class StateMachineAction : ScriptableObject
    {
        public abstract void Enter(IStateMachine stateMachine);
        public abstract void Exit(IStateMachine stateMachine);
    }
}
