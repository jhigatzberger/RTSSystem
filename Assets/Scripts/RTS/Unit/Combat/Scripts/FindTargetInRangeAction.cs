using RTSEngine.Core;
using RTSEngine.Core.AI;
using RTSEngine.Core.Combat;
using RTSEngine.Team;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "FindTargetInRangeAction", menuName = "RTS /AI/Actions/FindTargetInRangeAction")]
public class FindTargetInRangeAction : Action
{
    public override void Enter(IStateMachine stateMachine)
    {
        RTSBehaviour entity = stateMachine.Behaviour;
        IAttacker attacker = entity.GetComponent<IAttacker>();
        Collider[] potentialTargets = Physics.OverlapSphere(entity.transform.position, attacker.VisionRange, Context.teams[entity.Team].enemies);
        if (potentialTargets.Length != 0)
        {
            potentialTargets.OrderBy(c => (entity.transform.position - c.transform.position).sqrMagnitude);
            foreach (Collider c in potentialTargets)
            {
                if (c.gameObject.TryGetComponent(out IAttackable target))
                {
                    ((IAttacker)entity).Target = target;
                }
            }
        }
    }

    public override void Exit(IStateMachine entity)
    {
    }
}
