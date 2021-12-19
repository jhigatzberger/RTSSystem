using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RTS.Entity.AI
{
    public interface IStateMachine : IEntityExtension
    {
        public float TimeStamp { get; set; }
        public void ChangeState(State state);
        public void ResetState();
        public void UpdateState();
        public UnityEvent OnStateChainEnd { get; }
    }
}
