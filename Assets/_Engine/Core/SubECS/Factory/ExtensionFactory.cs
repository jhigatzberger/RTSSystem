using UnityEngine;

namespace JHiga.RTSEngine
{
    public abstract class ExtensionFactory : ScriptableObject, IExtensionFactory
    {
        public abstract IExtension Build(IExtendable extendable);
    }

}
