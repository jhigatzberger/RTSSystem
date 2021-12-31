using RTSEngine.Core.Movement;
using UnityEngine;

namespace RTSEngine.Core.AI
{
    [CreateAssetMenu(fileName = "ShouldStopMovingDecision", menuName = "RTS/AI/Decisions/ShouldStopMovingDecision")]
    public class ShouldStopMovingDecision : Decision
    {
        public float stoppingDistance = 0.1f; 
        public override bool Decide(IStateMachine stateMachine)
        {
            IMovable movable = stateMachine.Behaviour.GetComponent<IMovable>();            
            if (movable.Destination == null || Vector3.Distance(movable.Destination.Point, movable.Behaviour.transform.position) <= stoppingDistance)
                return true;
            return false;
        }
    }

}
