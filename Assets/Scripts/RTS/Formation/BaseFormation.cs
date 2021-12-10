using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS.Formation
{
    public abstract class BaseFormation
    {
        public abstract Vector3 GetPosition(Vector3 position, int index, int count);
    }
}