namespace JHiga.RTSEngine
{
    public interface IExtension
    {
        public IExtendable Extendable { set;  get; }
        public void Clear();
        public void Disable();
    }
}
