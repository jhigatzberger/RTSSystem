using JHiga.RTSEngine.StateMachine;
using UnityEngine;


[CreateAssetMenu(fileName = "WaitingDecision", menuName = "RTS/Behaviour/Decisions/WaitingDecision")]
public class WaitingDecision : Decision
{
    public float waitTime;
    public override bool Decide(IStateMachine stateMachine)
    {
        return LockStep.time - stateMachine.TimeStamp > waitTime;
    }
}


