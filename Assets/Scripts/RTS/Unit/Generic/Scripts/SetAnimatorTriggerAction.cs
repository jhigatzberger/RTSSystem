using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTSEngine.Entity.AI;


[CreateAssetMenu(fileName = "SetAnimatorTriggerAction", menuName = "RTS/AI/Actions/SetAnimatorTriggerAction")]
public class SetAnimatorTriggerAction : Action
{
    public string parameter;

    public override void Enter(IStateMachine stateMachine)
    {
        stateMachine.Entity.GetComponent<Animator>().SetTrigger(parameter);
    }

    public override void Exit(IStateMachine stateMachine)
    {
    }
}