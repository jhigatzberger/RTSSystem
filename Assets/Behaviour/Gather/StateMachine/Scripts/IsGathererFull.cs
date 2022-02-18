using JHiga.RTSEngine.StateMachine;
using UnityEngine;

namespace JHiga.RTSEngine.Gather
{
    [CreateAssetMenu(fileName = "IsGathererFull", menuName = "RTS/Behaviour/Decisions/IsGathererFull")]
    public class IsGathererFull : Decision
    {
        public override bool Decide(IStateMachine stateMachine)
        {
            IGatherer gatherer = stateMachine.Entity.GetExtension<IGatherer>();
            return gatherer.CurrentLoad >= gatherer.Capacity;
        }
    }
}