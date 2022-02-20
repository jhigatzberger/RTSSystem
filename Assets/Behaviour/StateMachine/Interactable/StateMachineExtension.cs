using JHiga.RTSEngine.CommandPattern;
using UnityEngine;

namespace JHiga.RTSEngine.StateMachine
{
    public class StateMachineExtension : BaseInteractableExtension<StateMachineProperties>, IStateMachine
    {
        private State currentState;
        private ICommandable _commandable;
        private ICommandable Commandable
        {
            get
            {
                if(_commandable==null)
                    _commandable = Entity.GetExtension<ICommandable>();
                return _commandable;
            }
        }
        public float TimeStamp { get; set; }
        public StateMachineExtension(IExtendableEntity entity, StateMachineProperties properties) : base(entity, properties) { }
        public void ChangeState(State state)
        {
            if (state == currentState)
                return;
            if (currentState != null)
                currentState.Exit(this);
            currentState = state;
            if (currentState != null)
                currentState.Enter(this);
            else if(Properties.commandable)
                Commandable.Finish();
            TimeStamp = LockStep.time;
            Debug.Log("STATE: " + currentState.name, Entity.MonoBehaviour);
        }
        public override void Enable()
        {
            LockStep.OnStep += UpdateState;
            ChangeState(Properties.defaultState);
        }
        public override void Disable()
        {
            LockStep.OnStep -= UpdateState;
            ChangeState(Properties.endState);
        }
        public override void Clear()
        {
            ChangeState(null);
        }
        public void UpdateState()
        {
            if (currentState != null)
                currentState.CheckTransitions(this);
            else if (Properties.defaultState != null)
                ChangeState(Properties.defaultState);
        }
    }
}
