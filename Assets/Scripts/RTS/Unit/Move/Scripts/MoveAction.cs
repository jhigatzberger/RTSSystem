using RTSEngine.Core;
using RTSEngine.Core.AI;
using RTSEngine.Core.Movement;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveAction", menuName = "RTS/AI/Actions/MoveAction")]
public class MoveAction : Action
{
    public override void Enter(IStateMachine stateMachine)
    {
        RTSBehaviour entity = stateMachine.Behaviour;
        IMovable movable = entity.GetComponent<IMovable>();
        movable.Move();
    }

    public override void Exit(IStateMachine stateMachine)
    {
        RTSBehaviour entity = stateMachine.Behaviour;
        IMovable movable = entity.GetComponent<IMovable>();
        movable.Stop();
    }
}

