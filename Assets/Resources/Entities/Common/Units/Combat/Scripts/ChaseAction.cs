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
        IExtendable entity = stateMachine.Entity;
        IMovable movable = entity.GetScriptableComponent<IMovable>();
        movable.Clear();
        IAttacker attacker = entity.GetScriptableComponent<IAttacker>();
        movable.Enqueue(new Target {entity = attacker.Target.Entity});
    }

    public override void Exit(IStateMachine stateMachine)
    {
        IExtendable entity = stateMachine.Entity;
        IMovable movable = entity.GetScriptableComponent<IMovable>();
        movable.Stop();
    }
}
