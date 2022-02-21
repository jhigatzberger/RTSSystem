using UnityEngine;

namespace JHiga.RTSEngine.StateMachine
{
    public class SetAnimatorIntAction : StateMachineAction
    {
        [SerializeField] private bool onEnter;
        [SerializeField] private string enterParameter;
        [SerializeField] private int enterValue;

        [SerializeField] private bool onExit;
        [SerializeField] private string exitParameter;
        [SerializeField] private int exitValue;

        public override void Enter(IStateMachine stateMachine)
        {
            if (onEnter)
                stateMachine.Entity.MonoBehaviour.GetComponent<Animator>().SetInteger(enterParameter, enterValue);
        }

        public override void Exit(IStateMachine stateMachine)
        {
            if (onExit)
                stateMachine.Entity.MonoBehaviour.GetComponent<Animator>().SetInteger(exitParameter, exitValue);
        }
    }
}