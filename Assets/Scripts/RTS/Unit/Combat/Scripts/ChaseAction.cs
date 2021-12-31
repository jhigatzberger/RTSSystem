using RTSEngine.Core;
using RTSEngine.Core.AI;
using RTSEngine.Core.Combat;
using RTSEngine.Core.Movement;
using UnityEngine;

[CreateAssetMenu(fileName = "ChaseAction", menuName = "RTS/AI/Actions/ChaseAction")]
public class ChaseAction : Action
{
    public override void Enter(IStateMachine stateMachine)
    {
        RTSBehaviour entity = stateMachine.Behaviour;
        IMovable movable = entity.GetExtension<IMovable>();
        movable.Clear();
        IAttacker attacker = entity.GetExtension<IAttacker>();
        movable.Enqueue(attacker.Target.Behaviour);
    }

    public override void Exit(IStateMachine stateMachine)
    {
        RTSBehaviour entity = stateMachine.Behaviour;
        IMovable movable = entity.GetExtension<IMovable>();
        movable.Stop();
    }
}
