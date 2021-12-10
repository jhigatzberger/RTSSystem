using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

namespace RTS
{
    public class StateMachine : MonoBehaviour
    {
        public BaseEntity controller;

        private Dictionary<Type, BaseState> states = new Dictionary<Type, BaseState>();
        [SerializeField] private BaseState[] _states;

        private BaseState currentState;

        private void Awake()
        {
            foreach (BaseState state in _states)
                states.Add(state.Super, state.Init(controller, gameObject));
            ChangeState(_states[0].Super);
        }

        private void Update()
        {
            if (currentState == null)
                return;
            Type next = currentState.Tick();
            if (next != null && next != currentState.Super)
                ChangeState(next);
        }

        private void ChangeState(Type state)
        {
            currentState = states[state];
        }

    }
}
