using UnityEngine;

namespace JHiga.RTSEngine
{
    public abstract class ExtensionFactory : ScriptableObject
    {
        public abstract IEntityExtension Build(IExtendableEntity extendable);
    }

}
