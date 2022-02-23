using UnityEngine;


namespace JHiga.RTSEngine.StateMachine
{
    public abstract class StateMachineDecision : ScriptableObject
    {
        public abstract bool Decide(IStateMachine stateMachine);
    }
}
