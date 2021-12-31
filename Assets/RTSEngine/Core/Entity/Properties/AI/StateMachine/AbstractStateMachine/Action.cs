using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSEngine.Core.AI
{   
    public abstract class Action : ScriptableObject
    {
        public abstract void Enter(IStateMachine stateMachine);
        public abstract void Exit(IStateMachine stateMachine);
    }
}
