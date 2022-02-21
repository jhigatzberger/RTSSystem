using JHiga.RTSEngine.StateMachine;
using UnityEngine;

namespace JHiga.RTSEngine
{
    public class SetChildWithNameActiveAction : StateMachineAction
    {
        public string childName;
        public bool changeOnEnter;
        public bool enterActive;
        public bool changeOnExit;
        public bool exitValue;
        public override void Enter(IStateMachine stateMachine)
        {
            if(changeOnEnter)
                stateMachine.Entity.MonoBehaviour.transform.Find(childName).gameObject.SetActive(enterActive);
        }

        public override void Exit(IStateMachine stateMachine)
        {
            if (changeOnExit)
                stateMachine.Entity.MonoBehaviour.transform.Find(childName).gameObject.SetActive(exitValue);
        }
    }
}