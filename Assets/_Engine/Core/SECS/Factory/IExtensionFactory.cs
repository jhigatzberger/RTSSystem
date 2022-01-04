namespace JHiga.RTSEngine
{
    public interface IExtensionFactory
    {
        public IExtension Build(IExtendable extendable);
    }
}
