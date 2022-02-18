using JHiga.RTSEngine.Combat;
using JHiga.RTSEngine;
using JHiga.RTSEngine.StateMachine;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "FindTargetInVisionAction", menuName = "RTS/Behaviour/Actions/FindTargetInVisionAction")]
public class FindTargetInVisionAction : Action
{
    public override void Enter(IStateMachine stateMachine)
    {
        IExtendableEntity entity = stateMachine.Entity;
        MonoBehaviour behaviour = entity.MonoBehaviour;
        IAttacker attacker = entity.GetExtension<IAttacker>();
        Collider[] potentialTargets = Physics.OverlapSphere(behaviour.transform.position, attacker.VisionRange, PlayerContext.players[entity.UniqueID.playerIndex].enemyMask);
        if (potentialTargets.Length != 0)
        {
            potentialTargets.OrderBy(c => (behaviour.transform.position - c.transform.position).sqrMagnitude);
            foreach (Collider c in potentialTargets)
            {
                if (c.gameObject.GetComponent<GameEntity>().TryGetExtension(out IAttackable target))
                {
                    attacker.Target = new Target
                    {
                        entity = target.Entity
                    };
                }
            }
        }
    }

    public override void Exit(IStateMachine entity)
    {
    }
}
