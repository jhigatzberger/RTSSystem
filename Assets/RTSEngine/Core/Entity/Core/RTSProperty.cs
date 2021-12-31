using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSEngine.Core
{
    public abstract class RTSProperty : ScriptableObject
    {
        public abstract IExtension Build(RTSBehaviour behaviour);
    }

}
