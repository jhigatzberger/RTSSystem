namespace JHiga.RTSEngine
{
    public abstract class BaseInteractableExtension<T> : IInteractableExtension where T : ExtensionFactory
    {
        public IExtendableInteractable Extendable { get; private set; }
        public T Properties { get; private set; }
        public BaseInteractableExtension(IExtendableInteractable extendable, T properties)
        {
            Extendable = extendable;
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
