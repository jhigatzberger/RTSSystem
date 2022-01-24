using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JHiga.RTSEngine.StateMachine
{
    [CreateAssetMenu(fileName = "DefaultStateMachine", menuName = "RTS/Entity/Properties/StateMachine")]
    public class StateMachineProperties : ExtensionFactory
    {
        public State defaultState;
        public State endState;
        public bool commandable;

        public override Type ExtensionType => typeof(IStateMachine);

        public override IEntityExtension Build(IExtendableEntity entity)
        {
            return new StateMachineExtension(entity, this);
        }
    }
}
