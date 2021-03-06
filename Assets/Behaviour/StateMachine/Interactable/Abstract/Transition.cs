using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JHiga.RTSEngine.StateMachine
{
    [CreateAssetMenu(fileName = "Transition", menuName = "RTS/Behaviour/Transition")]
    public class Transition : ScriptableObject
    {
        public StateMachineDecision decision;
        public State trueState;
        public State falseState;
    }
}