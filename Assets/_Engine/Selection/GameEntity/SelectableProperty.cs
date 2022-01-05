using JHiga.RTSEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JHiga.RTSEngine.Selection
{
    [CreateAssetMenu(fileName = "SelectableProperty", menuName = "RTS/Entity/Properties/SelectableProperty")]
    public class SelectableProperty : ExtensionFactory
    {
        public int priority;

        public override IExtension Build(IExtendable entity)
        {
            return new SelectableExtension(entity, priority);
        }
    }
}