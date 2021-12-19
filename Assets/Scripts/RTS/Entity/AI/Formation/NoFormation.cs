using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS.Entity.AI.Formation
{
    public class NoFormation : BaseFormation
    {
        public override Vector3 GetPosition(Vector3 destination, BaseEntity entity)
        {
            return destination;
        }

    }
}