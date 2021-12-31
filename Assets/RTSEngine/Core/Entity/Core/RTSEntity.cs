using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSEngine.Core
{
    [CreateAssetMenu(fileName = "Entity", menuName = "RTS/Entity/Entity")]
    public class RTSEntity : ScriptableObject
    {
        public RTSProperty[] properties;
        public Dictionary<Type, IExtension> Build(RTSBehaviour behaviour)
        {
            Dictionary<Type, IExtension> extensions = new Dictionary<Type, IExtension>();
            foreach (RTSProperty property in properties)
            {
                IExtension extension = property.Build(behaviour);
                extensions.Add(extension.GetType(), extension);
            }
            return extensions;
        }
    }

}
