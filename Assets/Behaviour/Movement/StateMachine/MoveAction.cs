using JHiga.RTSEngine.Movement;
using JHiga.RTSEngine;
using JHiga.RTSEngine.StateMachine;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveAction", menuName = "RTS/Behaviour/Actions/MoveAction")]
public class MoveAction : StateMachineAction
{
    public override void Enter(IStateMachine stateMachine)
    {
        IExtendableEntity entity = stateMachine.Entity;
        IMovable movable = entity.GetExtension<IMovable>();
        movable.Move();
    }

    public override void Exit(IStateMachine stateMachine)
    {
        IExtendableEntity entity = stateMachine.Entity;
        IMovable movable = entity.GetExtension<IMovable>();
        movable.Stop();
    }
}

