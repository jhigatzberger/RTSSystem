using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS.Entity.AI
{   
    public abstract class Action : ScriptableObject
    {
        public abstract void Enter(IStateMachine entity);
        public abstract void Exit(IStateMachine entity);
    }
}
