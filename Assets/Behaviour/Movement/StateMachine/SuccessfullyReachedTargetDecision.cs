using JHiga.RTSEngine.Movement;
using JHiga.RTSEngine.StateMachine;
using UnityEngine;

[CreateAssetMenu(fileName = "SuccessfullyReachedTargetDecision", menuName = "RTS/Behaviour/Decisions/SuccessfullyReachedTargetDecision")]
public class SuccessfullyReachedTargetDecision : Decision
{
    public float stoppingDistance = 0.1f;
    public override bool Decide(IStateMachine stateMachine)
    {
        IMovable movable = stateMachine.Entity.GetExtension<IMovable>();
        if (movable.Destination != null && movable.Distance <= stoppingDistance)
            return true;
        return false;
    }
}
