using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSEngine.Core.AI
{
    [CreateAssetMenu(fileName = "Transition", menuName = "RTS/AI/Transition")]
    public class Transition : ScriptableObject
    {
        public Decision decision;
        public State trueState;
        public State falseState;
    }
}