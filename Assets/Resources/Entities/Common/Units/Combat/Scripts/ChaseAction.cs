using JHiga.RTSEngine;
using JHiga.RTSEngine.Combat;
using JHiga.RTSEngine.Movement;
using JHiga.RTSEngine.StateMachine;
using UnityEngine;

[CreateAssetMenu(fileName = "ChaseAction", menuName = "RTS/Behaviour/Actions/ChaseAction")]
public class ChaseAction : Action
{
    public override void Enter(IStateMachine stateMachine)
    {
        IExtendableEntity entity = stateMachine.Entity;
        IMovable movable = entity.GetExtension<IMovable>();
        movable.Clear();
        IAttacker attacker = entity.GetExtension<IAttacker>();
        movable.Enqueue(new Target {entity = attacker.Target.Entity});
    }

    public override void Exit(IStateMachine stateMachine)
    {
        IExtendableEntity entity = stateMachine.Entity;
        IMovable movable = entity.GetExtension<IMovable>();
        movable.Stop();
    }
}
