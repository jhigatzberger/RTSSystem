using JHiga.RTSEngine;
using JHiga.RTSEngine.Combat;
using JHiga.RTSEngine.Movement;
using JHiga.RTSEngine.StateMachine;
using UnityEngine;

[CreateAssetMenu(fileName = "ChaseAction", menuName = "RTS/AI/Actions/ChaseAction")]
public class ChaseAction : Action
{
    public override void Enter(IStateMachine stateMachine)
    {
        IExtendable entity = stateMachine.Extendable;
        IMovable movable = entity.GetScriptableComponent<IMovable>();
        movable.Clear();
        IAttacker attacker = entity.GetScriptableComponent<IAttacker>();
        movable.Enqueue(attacker.Target.Extendable.MonoBehaviour.transform);
    }

    public override void Exit(IStateMachine stateMachine)
    {
        IExtendable entity = stateMachine.Extendable;
        IMovable movable = entity.GetScriptableComponent<IMovable>();
        movable.Stop();
    }
}
