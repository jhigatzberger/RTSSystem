using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSEngine.Core.AI.Formation
{
    public abstract class BaseFormation
    {
        public abstract Vector3 GetPosition(Vector3 destination, RTSBehaviour entity);
    }
}