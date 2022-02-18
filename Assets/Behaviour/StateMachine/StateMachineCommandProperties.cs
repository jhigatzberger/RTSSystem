using JHiga.RTSEngine.Selection;
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
        public override Target PackTarget(ICommandable commandable)
        {
            return new Target
            {
                position = SelectionContext.FirstOrNullHovered.MonoBehaviour.transform.position,
                entity = SelectionContext.FirstOrNullHovered
            };
        }
        protected virtual void BeforeStateChange(ICommandable commandable, ResolvedCommandReferences references)
        {
            commandable.Entity.GetExtension<ITargeter>().Target = references.target;
        }
    }

}
