using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSEngine.Core.Selection
{
    [CreateAssetMenu(fileName = "SelectableProperty", menuName = "RTS/Entity/Properties/SelectableProperty")]
    public class SelectableProperty : RTSProperty
    {
        public string rendererTag;
        public int priority;

        public override IExtension Build(RTSBehaviour behaviour)
        {
            return new SelectableExtension(behaviour, priority, rendererTag);
        }
    }
}