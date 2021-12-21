using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RTS.Entity.AI
{
    [RequireComponent(typeof(BaseEntity))]
    public class StatedEntity : MonoBehaviour, IStateMachine
    {
        private State currentState;
        [SerializeField] private State defaultState;
        public float TimeStamp { get; set; }
        private BaseEntity _entity;
        public BaseEntity Entity => _entity;

        public UnityEvent OnStateChainEnd => _onStateChainEnd;
        [SerializeField] private UnityEvent _onStateChainEnd;
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

        private void Awake()
        {
            LockStep.OnStep += UpdateState;
            _entity = GetComponent<BaseEntity>();
            _entity.OnExitScene += CleanUp;
        }

        public void CleanUp()
        {
            LockStep.OnStep -= UpdateState;
            _entity.OnExitScene -= CleanUp;
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

        public void OnExitScene()
        {
            CleanUp();
            enabled = false;
        }
    }

}
