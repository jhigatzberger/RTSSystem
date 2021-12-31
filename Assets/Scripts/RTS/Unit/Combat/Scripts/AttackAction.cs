using RTSEngine.Core.AI;
using RTSEngine.Core.Combat;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackAction", menuName = "RTS/AI/Actions/AttackAction")]
public class AttackAction : Action
{
    public override void Enter(IStateMachine entity)
    {
        entity.Behaviour.GetExtension<IAttacker>().Attack();
    }

    public override void Exit(IStateMachine entity)
    {
    }
        
}
