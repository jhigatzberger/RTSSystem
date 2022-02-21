using JHiga.RTSEngine;
using JHiga.RTSEngine.Combat;
using JHiga.RTSEngine.Movement;
using JHiga.RTSEngine.StateMachine;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveToTarget", menuName = "RTS/Behaviour/Actions/MoveToTarget")]
public class MoveToTarget : StateMachineAction
{
    public override void Enter(IStateMachine stateMachine)
    {
        IExtendableEntity entity = stateMachine.Entity;
        IMovable movable = entity.GetExtension<IMovable>();
        movable.Clear();
        ITargeter targeter = entity.GetExtension<ITargeter>();
        movable.Enqueue(targeter.Target.Value);
    }

    public override void Exit(IStateMachine stateMachine)
    {
        IExtendableEntity entity = stateMachine.Entity;
        IMovable movable = entity.GetExtension<IMovable>();
        movable.Stop();
    }
}
