using System;
using UnityEngine;
using UnityEngine.Events;

namespace RTSEngine.Core.AI
{
    public class StateMachineExtension : RTSExtension, IStateMachine
    {
        public event System.Action OnStateChainEnd;

        private State currentState;
        private State defaultState;

        public StateMachineExtension(RTSBehaviour behaviour, State defaultState) : base(behaviour)
        {
            this.defaultState = defaultState;
            LockStep.OnStep += UpdateState;
            Behaviour.OnClear += ResetState;
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
            else
                OnStateChainEnd.Invoke();
            TimeStamp = LockStep.time;
        }   
        protected override void OnExitScene()
        {
            Behaviour.OnClear -= ResetState;
            LockStep.OnStep -= UpdateState;
            if (currentState != null)
                currentState.Exit(this);
        }

        public void ResetState()
        {
            ChangeState(null);
        }
        public void UpdateState()
        {
            if (currentState != null)
                currentState.CheckTransitions(this);
            else if (defaultState != null)
                ChangeState(defaultState);
        }
    }
}
