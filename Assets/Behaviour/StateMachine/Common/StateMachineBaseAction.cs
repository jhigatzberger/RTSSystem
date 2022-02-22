using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JHiga.RTSEngine.StateMachine
{
    [CreateAssetMenu(fileName = "StateMachineBaseAction", menuName = "RTS/Behaviour/Actions/StateMachineBaseAction")]
    public class StateMachineBaseAction : StateMachineAction
    {
        [SerializeField] private BaseAction _baseAction;

        public override void Enter(IStateMachine stateMachine)
        {
            _baseAction.Run(stateMachine.Entity);
        }

        public override void Exit(IStateMachine stateMachine)
        {
            _baseAction.Stop(stateMachine.Entity);
        }
    }
}