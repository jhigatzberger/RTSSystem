using RTSEngine.Core.Combat;
using UnityEngine;

namespace RTSEngine.Core.AI
{
    [CreateAssetMenu(fileName = "IsAttackingDecision", menuName = "RTS/AI/Decisions/IsAttackingDecision")]
    public class IsAttackingDecision : Decision
    {
        public override bool Decide(IStateMachine stateMachine)
        {
            return stateMachine.Behaviour.GetExtension<IAttacker>().IsAttacking;
        }
    }
}
