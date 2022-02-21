using JHiga.RTSEngine.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JHiga.RTSEngine.Gathering
{
    public class DropoffAction : StateMachineAction
    {
        public override void Enter(IStateMachine stateMachine)
        {
            stateMachine.Entity.GetExtension<IGatherer>().DropOff();
        }

        public override void Exit(IStateMachine stateMachine)
        {
        }
    }
}