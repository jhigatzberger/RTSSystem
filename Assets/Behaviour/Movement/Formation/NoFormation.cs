using JHiga.RTSEngine;
using UnityEngine;

namespace JHiga.RTSEngine.AI.Formation
{
    public class NoFormation : BaseFormation
    {
        public override Vector3 GetPosition(Vector3 destination, IExtendable entity)
        {
            return destination;
        }

    }
}