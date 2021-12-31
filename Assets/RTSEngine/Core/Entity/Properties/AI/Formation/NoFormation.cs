using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSEngine.Core.AI.Formation
{
    public class NoFormation : BaseFormation
    {
        public override Vector3 GetPosition(Vector3 destination, RTSBehaviour entity)
        {
            return destination;
        }

    }
}