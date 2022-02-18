using System;
namespace JHiga.RTSEngine
{
    public class TargeterProperties : ExtensionFactory
    {
        public override Type ExtensionType => typeof(ITargeter);

        public override IEntityExtension Build(IExtendableEntity extendable)
        {
            return new TargeterExtension(extendable, this);
        }

    }
}