using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSEngine.Core
{
    [CreateAssetMenu(fileName = "Entity", menuName = "RTS/Entity/Entity")]
    public class RTSEntity : ScriptableObject
    {
        public RTSProperty[] properties;
        public IExtension[] Build(RTSBehaviour behaviour)
        {
            IExtension[] extensions = new RTSExtension[properties.Length];
            for (int i = 0; i < extensions.Length; i++)
                extensions[i] = properties[i].Build(behaviour);
            return extensions;
        }
    }

}
