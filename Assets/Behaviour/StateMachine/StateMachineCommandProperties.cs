using JHiga.RTSEngine.StateMachine;

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
            return Target.FromContext;
        }
        protected virtual void BeforeStateChange(ICommandable commandable, ResolvedCommandReferences references)
        {
            commandable.Entity.GetExtension<ITargeter>().Target = references.target;
        }
    }

}
