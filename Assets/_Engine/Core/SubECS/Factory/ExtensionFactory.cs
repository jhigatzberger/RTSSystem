using UnityEngine;

namespace JHiga.RTSEngine
{
    public abstract class ExtensionFactory : ScriptableObject
    {
        public abstract IInteractableExtension Build(IExtendableInteractable extendable);
    }

}
