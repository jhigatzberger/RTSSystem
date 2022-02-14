using JHiga.RTSEngine.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JHiga.RTSEngine.CommandPattern
{
    public abstract class StateMachineCommandProperties : CommandProperties
    {
        public State state;
        public override void Execute(ICommandable commandable, ResolvedCommandReferences references)
        {
            BeforeStateChange(commandable, references);
            commandable.Entity.GetExtension<IStateMachine>().ChangeState(state);
        }
        protected abstract void BeforeStateChange(ICommandable commandable, ResolvedCommandReferences references);
    }

}
