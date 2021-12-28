using RTSEngine.Entity.AI;
using RTSEngine.Entity.Combat;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackAction", menuName = "RTS/AI/Actions/AttackAction")]
public class AttackAction : Action
{
    public override void Enter(IStateMachine entity)
    {
        entity.Entity.GetComponent<IAttacker>().Attack();
    }

    public override void Exit(IStateMachine entity)
    {
    }
        
}
