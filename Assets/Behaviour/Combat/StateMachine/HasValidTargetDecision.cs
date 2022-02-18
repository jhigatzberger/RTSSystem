using JHiga.RTSEngine.StateMachine;
using UnityEngine;
using JHiga.RTSEngine;

[CreateAssetMenu(fileName = "HasValidTargetDecision", menuName = "RTS/Behaviour/Decisions/HasValidTargetDecision")]
public class HasValidTargetDecision : Decision
{
    public override bool Decide(IStateMachine stateMachine)
    {
        ITargeter targeter = stateMachine.Entity.GetExtension<ITargeter>();
        return targeter.Target.HasValue && targeter.Target.Value.HasActiveEntity;
    }
}

