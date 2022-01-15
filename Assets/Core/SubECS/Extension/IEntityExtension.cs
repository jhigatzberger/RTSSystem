namespace JHiga.RTSEngine
{
    public interface IEntityExtension
    {
        public IExtendableEntity Entity { get; }
        public void Clear();
        public void Enable();
        public void Disable();
    }
}
