using UnityEngine;

namespace JHiga.RTSEngine
{
    public abstract class BaseAction : ScriptableObject
    {
        public abstract void Run(IExtendableEntity entity);
        public abstract void Stop(IExtendableEntity entity);
    }
}