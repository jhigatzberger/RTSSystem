namespace JHiga.RTSEngine
{
    public interface IInteractableExtension
    {
        public IExtendable Entity { get; }
        public void Clear();
        public void Enable();
        public void Disable();
    }
}
