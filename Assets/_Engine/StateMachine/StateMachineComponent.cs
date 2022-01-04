using JHiga.RTSEngine.CommandPattern;

namespace JHiga.RTSEngine.StateMachine
{
    public class StateMachineComponent : Extension, IStateMachine
    {
        private State currentState;
        private StateMachineProperty property;
        private ICommandable _commandable;
        private ICommandable Commandable
        {
            get
            {
                if(_commandable==null)
                    _commandable = Extendable.GetScriptableComponent<ICommandable>();
                return _commandable;
            }
        }

        public StateMachineComponent(IExtendable entity, StateMachineProperty property) : base(entity)
        {
            this.property = property;
            LockStep.OnStep += UpdateState;
        }

        public float TimeStamp { get; set; }
        public void ChangeState(State state)
        {
            if (state == currentState)
                return;
            if (currentState != null)
                currentState.Exit(this);
            currentState = state;
            if (currentState != null)
                currentState.Enter(this);
            else if(property.commandable)
                Commandable.Finish();
            TimeStamp = LockStep.time;
        }   
        public override void Disable()
        {
            LockStep.OnStep -= UpdateState;
            if (currentState != null)
                currentState.Exit(this);
        }

        public override void Clear()
        {
            ChangeState(null);
        }
        public void UpdateState()
        {
            if (currentState != null)
                currentState.CheckTransitions(this);
            else if (property.defaultState != null)
                ChangeState(property.defaultState);
        }
    }
}
