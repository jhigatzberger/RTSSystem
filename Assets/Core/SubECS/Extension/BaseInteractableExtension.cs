namespace JHiga.RTSEngine
{
    public abstract class BaseInteractableExtension<T> : IEntityExtension where T : ExtensionFactory
    {
        public IExtendableEntity Entity { get; private set; }
        public T Properties { get; private set; }
        public BaseInteractableExtension(IExtendableEntity entity, T properties)
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
