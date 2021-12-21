using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSEngine.Entity.AI
{
    [CreateAssetMenu(fileName = "InRangeDecision", menuName = "RTS/AI/Decisions/InRangeDecision")]
    public class InRangeDecision : Decision
    {
        public override bool Decide(IStateMachine entity)
        {
            return entity.Entity.GetComponent<IAttacker>().IsInRange;
        }
    }
}
