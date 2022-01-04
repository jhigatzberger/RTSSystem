using JHiga.RTSEngine.Movement;
using JHiga.RTSEngine;
using JHiga.RTSEngine.StateMachine;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveAction", menuName = "RTS/AI/Actions/MoveAction")]
public class MoveAction : Action
{
    public override void Enter(IStateMachine stateMachine)
    {
        IExtendable entity = stateMachine.Extendable;
        IMovable movable = entity.GetScriptableComponent<IMovable>();
        movable.Move();
    }

    public override void Exit(IStateMachine stateMachine)
    {
        IExtendable entity = stateMachine.Extendable;
        IMovable movable = entity.GetScriptableComponent<IMovable>();
        movable.Stop();
    }
}

