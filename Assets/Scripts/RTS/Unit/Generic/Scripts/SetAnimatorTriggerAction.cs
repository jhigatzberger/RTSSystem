using UnityEngine;
using RTSEngine.Core.AI;


[CreateAssetMenu(fileName = "SetAnimatorTriggerAction", menuName = "RTS/AI/Actions/SetAnimatorTriggerAction")]
public class SetAnimatorTriggerAction : Action
{
    public string parameter;

    public override void Enter(IStateMachine stateMachine)
    {
        stateMachine.Behaviour.GetComponent<Animator>().SetTrigger(parameter);
    }

    public override void Exit(IStateMachine stateMachine)
    {
    }
}