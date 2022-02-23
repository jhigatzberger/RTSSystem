using UnityEngine;

namespace JHiga.RTSEngine
{
    public abstract class BaseDecision : ScriptableObject
    {
        public abstract bool Decide(IExtendableEntity entity);
    }
}
