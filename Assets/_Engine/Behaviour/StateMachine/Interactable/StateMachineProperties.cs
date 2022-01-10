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
        public override IInteractableExtension Build(IExtendableInteractable entity)
        {
            return new StateMachineExtension(entity, this);
        }
    }
}
