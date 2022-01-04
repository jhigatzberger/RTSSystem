using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JHiga.RTSEngine.StateMachine
{   
    public abstract class Action : ScriptableObject
    {
        public abstract void Enter(IStateMachine stateMachine);
        public abstract void Exit(IStateMachine stateMachine);
    }
}
