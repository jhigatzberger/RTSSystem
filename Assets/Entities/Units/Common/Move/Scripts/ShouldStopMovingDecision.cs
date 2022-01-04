using JHiga.RTSEngine.StateMachine;
using JHiga.RTSEngine.Movement;
using UnityEngine;


[CreateAssetMenu(fileName = "ShouldStopMovingDecision", menuName = "RTS/AI/Decisions/ShouldStopMovingDecision")]
public class ShouldStopMovingDecision : Decision
{
    public float stoppingDistance = 0.1f; 
    public override bool Decide(IStateMachine stateMachine)
    {
        IMovable movable = stateMachine.Extendable.GetScriptableComponent<IMovable>();            
        if (movable.Destination == null || Vector3.Distance(movable.Destination.Point, movable.Extendable.MonoBehaviour.transform.position) <= stoppingDistance)
            return true;
        return false;
    }
}


