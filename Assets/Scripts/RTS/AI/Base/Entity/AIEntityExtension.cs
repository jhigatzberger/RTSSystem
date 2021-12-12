using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS.AI
{
    [RequireComponent(typeof(AIEntity))]
    public abstract class AIEntityExtension : MonoBehaviour
    {
        public abstract int Priority { get; }
        public abstract bool Applicable { get; }
        public abstract Command Command { get; }
        public abstract CommandData ContextPoll();
    }
}