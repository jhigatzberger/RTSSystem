namespace JHiga.RTSEngine
{
    public abstract class BaseInteractableExtension<T> : IInteractableExtension where T : ExtensionFactory
    {
        public IExtendable Entity { get; private set; }
        public T Properties { get; private set; }
        public BaseInteractableExtension(IExtendable entity, T properties)
        {
            Entity = entity;
            Properties = properties;
        }
        public virtual void Disable()
        {
            Clear();
        }
        public virtual void Enable()
        {

        }
        public virtual void Clear()
        {

        }
    }
}
