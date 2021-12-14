using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS.Entity.AI
{
    [CreateAssetMenu(fileName = "WaitingDecision", menuName = "RTS/AI/Decisions/WaitingDecision")]
    public class WaitingDecision : Decision
    {
        public float waitTime;
        public override bool Decide(AIEntity entity)
        {
            return Time.time - entity.stateTimeStamp > waitTime;
        }
    }

}
