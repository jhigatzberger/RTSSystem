using RTSEngine.Core;
using RTSEngine.Core.AI;
using RTSEngine.Core.Combat;
using RTSEngine.Team;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "FindTargetInVisionAction", menuName = "RTS /AI/Actions/FindTargetInVisionAction")]
public class FindTargetInVisionAction : Action
{
    public override void Enter(IStateMachine stateMachine)
    {
        RTSBehaviour behaviour = stateMachine.Behaviour;
        IAttacker attacker = behaviour.GetExtension<IAttacker>();
        Collider[] potentialTargets = Physics.OverlapSphere(behaviour.transform.position, attacker.VisionRange, TeamContext.teams[behaviour.Team].enemies);
        if (potentialTargets.Length != 0)
        {
            potentialTargets.OrderBy(c => (behaviour.transform.position - c.transform.position).sqrMagnitude);
            foreach (Collider c in potentialTargets)
            {
                if (c.gameObject.GetComponent<RTSBehaviour>().TryGetExtension(out IAttackable target))
                {
                    attacker.Target = target;
                }
            }
        }
    }

    public override void Exit(IStateMachine entity)
    {
    }
}
