using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSEngine.Core.AI
{
    public abstract class Decision : ScriptableObject
    {
        public abstract bool Decide(IStateMachine stateMachine);
    }
}