using JHiga.RTSEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JHiga.RTSEngine.Selection
{
    [CreateAssetMenu(fileName = "DefaultSelectable", menuName = "RTS/Entity/Properties/Selectable")]
    public class SelectableProperties : ExtensionFactory
    {
        public int priority;
        public float scale = 1;
        public GameObject selectionBehaviourPrefab;

        public override Type ExtensionType => typeof(ISelectable);

        public override IEntityExtension Build(IExtendableEntity entity)
        {
            return new SelectableExtension(entity, this);
        }
    }
}