using JHiga.RTSEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JHiga.RTSEngine.Selection
{
    [CreateAssetMenu(fileName = "DefaultSelectable", menuName = "RTS/Entity/Properties/Selectable")]
    public class SelectableProperties : ExtensionFactory
    {
        public int priority;

        public override IInteractableExtension Build(IExtendableInteractable entity)
        {
            return new SelectableExtension(entity, this);
        }
    }
}