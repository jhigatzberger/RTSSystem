namespace JHiga.RTSEngine
{
    public interface IInteractableExtension
    {
        public IExtendableInteractable Extendable { get; }
        public void Clear();
        public void Enable();
        public void Disable();
    }
}
