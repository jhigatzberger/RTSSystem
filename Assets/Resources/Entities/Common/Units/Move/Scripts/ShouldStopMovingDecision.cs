using JHiga.RTSEngine.StateMachine;
using JHiga.RTSEngine.Movement;
using UnityEngine;


[CreateAssetMenu(fileName = "ShouldStopMovingDecision", menuName = "RTS/Behaviour/Decisions/ShouldStopMovingDecision")]
public class ShouldStopMovingDecision : Decision
{
    public float stoppingDistance = 0.1f; 
    public override bool Decide(IStateMachine stateMachine)
    {
        IMovable movable = stateMachine.Entity.GetExtension<IMovable>();            
        if (movable.Destination == null || movable.Distance <= stoppingDistance)
            return true;
        return false;
    }
}


