using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JHiga.RTSEngine.StateMachine
{
    [CreateAssetMenu(fileName = "StateMachineProperty", menuName = "RTS/Entity/Properties/StateMachineProperty")]
    public class StateMachineProperty : ExtensionFactory
    {
        public State defaultState;
        public bool commandable;
        public override IExtension Build(IExtendable entity)
        {
            return new StateMachineComponent(entity, this);
        }
    }
}
