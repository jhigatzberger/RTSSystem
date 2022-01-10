using JHiga.RTSEngine;
using UnityEngine;

namespace JHiga.RTSEngine.AI.Formation
{
    public abstract class BaseFormation
    {
        public abstract Vector3 GetPosition(Vector3 destination, IExtendableInteractable entity);
    }
}