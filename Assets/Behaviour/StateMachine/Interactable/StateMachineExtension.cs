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
        public StateMachineExtension(IExtendableEntity entity, StateMachineProperties properties) : base(entity, properties)
        {
            TimeStamp = LockStep.time;
        }
        public void ChangeState(State state)
        {
            if(state != null)
                Debug.Log(state.name, Entity.MonoBehaviour);
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
