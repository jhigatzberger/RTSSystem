using UnityEngine;

namespace RTSEngine.Core.AI
{
    [CreateAssetMenu(fileName = "WaitingDecision", menuName = "RTS/AI/Decisions/WaitingDecision")]
    public class WaitingDecision : Decision
    {
        public float waitTime;
        public override bool Decide(IStateMachine stateMachine)
        {
            return LockStep.time - stateMachine.TimeStamp > waitTime;
        }
    }

}
