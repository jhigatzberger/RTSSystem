using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS.AI
{
    public abstract class Decision : ScriptableObject
    {
        public abstract bool Decide(AIEntity entity);
    }
}