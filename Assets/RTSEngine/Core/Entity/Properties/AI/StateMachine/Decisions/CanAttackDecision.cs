using RTSEngine.Core.Combat;
using UnityEngine;

namespace RTSEngine.Core.AI
{
    [CreateAssetMenu(fileName = "CanAttackDecision", menuName = "RTS/AI/Decisions/CanAttackDecision")]
    public class CanAttackDecision : Decision
    {
        public override bool Decide(IStateMachine stateMachine)
        {
            return stateMachine.Behaviour.GetComponent<IAttacker>().CanAttack;
        }
    }
}
