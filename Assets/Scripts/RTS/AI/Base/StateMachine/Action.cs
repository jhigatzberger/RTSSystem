using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS.AI
{   
    public abstract class Action : ScriptableObject
    {
        public abstract void Enter(AIEntity entity);
        public abstract void Exit(AIEntity entity);
    }
}