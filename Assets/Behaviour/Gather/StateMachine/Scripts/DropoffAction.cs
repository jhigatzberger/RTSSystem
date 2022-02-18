using JHiga.RTSEngine.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JHiga.RTSEngine.Gather
{
    public class DropoffAction : Action
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