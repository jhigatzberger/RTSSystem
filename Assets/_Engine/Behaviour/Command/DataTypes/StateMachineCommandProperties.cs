using JHiga.RTSEngine.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JHiga.RTSEngine.CommandPattern
{
    public abstract class StateMachineCommandProperties : CommandProperties
    {
        public State state;
        public override void Execute(ICommandable commandable, CompiledCommand data)
        {
            OnExecute(commandable, data);
            commandable.Extendable.GetScriptableComponent<IStateMachine>().ChangeState(state);
        }
        protected abstract void OnExecute(ICommandable commandable, CompiledCommand data);
    }

}
