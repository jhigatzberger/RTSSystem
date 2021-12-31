using RTSEngine.Core.Combat;
using UnityEngine;

namespace RTSEngine.Core.AI
{
    [CreateAssetMenu(fileName = "HasValidTargetDecision", menuName = "RTS/AI/Decisions/HasValidTargetDecision")]
    public class HasValidTargetDecision : Decision
    {
        public override bool Decide(IStateMachine stateMachine)
        {
            IAttacker attacker = stateMachine.Behaviour.GetExtension<IAttacker>();
            return attacker.Target != null && attacker.Target.IsAlive;
        }
    }

}
