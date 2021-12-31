using RTSEngine.Core.Combat;
using UnityEngine;

namespace RTSEngine.Core.AI
{
    [CreateAssetMenu(fileName = "ShouldChaseDecision", menuName = "RTS/AI/Decisions/ShouldChaseDecision")]
    public class ShouldChaseDecision : Decision
    {
        public override bool Decide(IStateMachine stateMachine)
        {
            IAttacker attacker = stateMachine.Behaviour.GetExtension<IAttacker>();
            return attacker.Target != null && attacker.Target.IsAlive && !attacker.IsInRange;
        }
    }
}
