using UnityEngine;

namespace JHiga.RTSEngine
{
    public abstract class EntityFactory<T> : ScriptableObject where T : IExtendable
    {
        public int PlayerId { get; private set; }
        public int Index { get; private set; }
        public abstract T Get(int index);
        public abstract ExtensionFactory[] ExtensionFactories { get; protected set; }
        public abstract int GenerateEntityID();
        public static EntityFactory<T> Copy(EntityFactory<T> original, int playerId, int index, ExtensionFactory[] properties)
        {
            EntityFactory<T> factory = Instantiate(original);
            factory.PlayerId = playerId;
            factory.Index = index;
            factory.ExtensionFactories = properties;
            Debug.Log(index + " index copied!");
            return factory;
        }
        public IInteractableExtension[] Build(IExtendable entity)
        {
            IInteractableExtension[] extensions = new IInteractableExtension[ExtensionFactories.Length];
            for (int i = 0; i < extensions.Length; i++)
                extensions[i] = ExtensionFactories[i].Build(entity);
            return extensions;
        }
        public abstract T Spawn(Vector3 position, int id);
    }

}
