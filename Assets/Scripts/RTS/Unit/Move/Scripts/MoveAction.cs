using RTSEngine.Entity;
using RTSEngine.Entity.AI;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveAction", menuName = "RTS/AI/Actions/MoveAction")]
public class MoveAction : Action
{
    public override void Enter(IStateMachine stateMachine)
    {
        BaseEntity entity = stateMachine.Entity;
        IMovable movable = entity.GetComponent<IMovable>();
        movable.Move();
    }

    public override void Exit(IStateMachine stateMachine)
    {
        BaseEntity entity = stateMachine.Entity;
        IMovable movable = entity.GetComponent<IMovable>();
        movable.Stop();
    }
}

