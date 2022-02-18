using System;

namespace JHiga.RTSEngine.Gather
{
    public class GathererProperties : ExtensionFactory
    {
        public int carryingCapacity;
        public float speed;
        public override Type ExtensionType => typeof(IGatherer);
        public override IEntityExtension Build(IExtendableEntity extendable)
        {
            return new GathererExtension(extendable, this);
        }
    }
}