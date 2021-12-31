using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSEngine.Core.AI
{
    [CreateAssetMenu(fileName = "StateMachineProperty", menuName = "RTS/Entity/Properties/StateMachineProperty")]
    public class StateMachineProperty : RTSProperty
    {
        public State defaultState;
        public override IExtension Build(RTSBehaviour behaviour)
        {
            return new StateMachineExtension(behaviour, defaultState);
        }
    }
}
