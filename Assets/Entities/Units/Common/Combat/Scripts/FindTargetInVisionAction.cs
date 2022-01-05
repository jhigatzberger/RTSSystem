using JHiga.RTSEngine.Combat;
using JHiga.RTSEngine;
using JHiga.RTSEngine.StateMachine;
using JHiga.RTSEngine.Team;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "FindTargetInVisionAction", menuName = "RTS /AI/Actions/FindTargetInVisionAction")]
public class FindTargetInVisionAction : Action
{
    public override void Enter(IStateMachine stateMachine)
    {
        IExtendable entity = stateMachine.Extendable;
        MonoBehaviour behaviour = entity.MonoBehaviour;
        IAttacker attacker = entity.GetScriptableComponent<IAttacker>();
        Collider[] potentialTargets = Physics.OverlapSphere(behaviour.transform.position, attacker.VisionRange, TeamContext.teams[entity.PlayerId].enemies);
        if (potentialTargets.Length != 0)
        {
            potentialTargets.OrderBy(c => (behaviour.transform.position - c.transform.position).sqrMagnitude);
            foreach (Collider c in potentialTargets)
            {
                if (c.gameObject.GetComponent<GameEntity>().TryGetScriptableComponent(out IAttackable target))
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
