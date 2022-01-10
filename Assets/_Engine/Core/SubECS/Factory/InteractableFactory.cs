using UnityEngine;

namespace JHiga.RTSEngine
{
    public abstract class InteractableFactory<T> : ScriptableObject where T : IExtendableInteractable
    {
        public abstract ExtensionFactory[] ExtensionFactories { get; }
        public IInteractableExtension[] Build(IExtendableInteractable entity)
        {
            IInteractableExtension[] extensions = new IInteractableExtension[ExtensionFactories.Length];
            for (int i = 0; i < extensions.Length; i++)
                extensions[i] = ExtensionFactories[i].Build(entity);
            return extensions;
        }
        public abstract T Spawn(Vector3 position, int id, int team);
    }

}
