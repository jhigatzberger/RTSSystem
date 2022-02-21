using UnityEngine;

namespace JHiga.RTSEngine.StateMachine
{
    public class SetAnimatorTriggerAction : StateMachineAction
    {
        [SerializeField] private bool onEnter;
        [SerializeField] private string enterParameter;

        [SerializeField] private bool onExit;
        [SerializeField] private string exitParameter;

        public override void Enter(IStateMachine stateMachine)
        {
            Animator anim = stateMachine.Entity.MonoBehaviour.GetComponent<Animator>();
            if (onEnter)
                anim.SetTrigger(enterParameter);
            if (onExit)
                anim.ResetTrigger(exitParameter);
            
        }

        public override void Exit(IStateMachine stateMachine)
        {
            Animator anim = stateMachine.Entity.MonoBehaviour.GetComponent<Animator>();
            if (onExit)
                anim.SetTrigger(exitParameter);
            if (onEnter)
                anim.ResetTrigger(enterParameter);
        }
    }
}