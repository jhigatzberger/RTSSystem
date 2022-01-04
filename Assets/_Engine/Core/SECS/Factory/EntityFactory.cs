using UnityEngine;

namespace JHiga.RTSEngine
{
    public abstract class EntityFactory : ScriptableObject
    {
        public abstract IExtensionFactory[] ComponentFactories { get; }
        public IExtension[] Build(IExtendable entity)
        {
            IExtension[] extensions = new IExtension[ComponentFactories.Length];
            for (int i = 0; i < extensions.Length; i++)
                extensions[i] = ComponentFactories[i].Build(entity);
            return extensions;
        }
    }

}
