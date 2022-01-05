namespace JHiga.RTSEngine
{
    public abstract class Extension : IExtension
    {
        public IExtendable Extendable { get; set; }
        public Extension(IExtendable extendable)
        {
            Extendable = extendable;
        }
        public virtual void Disable()
        {
            Clear();
        }
        public virtual void Clear()
        {

        }
    }
}
