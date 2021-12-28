using UnityEngine;

namespace RTSEngine.Entity.AI
{
    [CreateAssetMenu(fileName = "ShouldStopMovingDecision", menuName = "RTS/AI/Decisions/ShouldStopMovingDecision")]
    public class ShouldStopMovingDecision : Decision
    {
        public float stoppingDistance = 0.1f; 
        public override bool Decide(IStateMachine stateMachine)
        {
            IMovable movable = stateMachine.Entity.GetComponent<IMovable>();            
            if (movable.Destination == null || Vector3.Distance(movable.Destination.Point, movable.Entity.transform.position) <= stoppingDistance)
                return true;
            return false;
        }
    }

}
