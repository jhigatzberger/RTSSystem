using JHiga.RTSEngine.StateMachine;
using UnityEngine;

namespace JHiga.RTSEngine.Gathering
{
    [CreateAssetMenu(fileName = "IsGathererFull", menuName = "RTS/Behaviour/Decisions/IsGathererFull")]
    public class IsGathererFull : StateMachineDecision
    {
        public override bool Decide(IStateMachine stateMachine)
        {
            IGatherer gatherer = stateMachine.Entity.GetExtension<IGatherer>();
            return gatherer.CurrentLoad >= gatherer.Capacity;
        }
    }
}