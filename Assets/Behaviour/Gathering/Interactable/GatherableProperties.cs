using System;

namespace JHiga.RTSEngine.Gathering
{
    public class GatherableProperties : ExtensionFactory
    {
        public override Type ExtensionType => typeof(IGatherable);
        public int rate;
        public int resourceType;
        public IGatherable.StorageType storageType;
        public int totalStorage;
        public override IEntityExtension Build(IExtendableEntity extendable)
        {
            return new GatherableExtension(extendable, this);
        }
    }
}