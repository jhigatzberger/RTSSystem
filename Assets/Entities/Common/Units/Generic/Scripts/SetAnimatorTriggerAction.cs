using UnityEngine;
using JHiga.RTSEngine.StateMachine;

[CreateAssetMenu(fileName = "SetAnimatorTriggerAction", menuName = "RTS/Behaviour/Actions/SetAnimatorTriggerAction")]
public class SetAnimatorTriggerAction : Action
{
    public bool onEnter;
    public string enterParameter;

    public bool onExit;
    public string exitParameter;

    public override void Enter(IStateMachine stateMachine)
    {
        if(onEnter)
            stateMachine.Extendable.MonoBehaviour.GetComponent<Animator>().SetTrigger(enterParameter);
    }

    public override void Exit(IStateMachine stateMachine)
    {
        if (onExit)
            stateMachine.Extendable.MonoBehaviour.GetComponent<Animator>().SetTrigger(exitParameter);
    }
}