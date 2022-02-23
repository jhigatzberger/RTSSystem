using UnityEngine;

namespace JHiga.RTSEngine.StateMachine
{
    public class StateMachineBaseDecision : StateMachineDecision
    {
        [SerializeField] private BaseDecision _decision;
        public override bool Decide(IStateMachine stateMachine)
        {
            return _decision.Decide(stateMachine.Entity);
        }
    }

}
