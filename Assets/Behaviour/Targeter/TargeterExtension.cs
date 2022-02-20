using System;
namespace JHiga.RTSEngine
{
    public class TargeterExtension : BaseInteractableExtension<TargeterProperties>,  ITargeter
    {
        public TargeterExtension(IExtendableEntity entity, TargeterProperties properties) : base(entity, properties)
        {
        }
        public Target? Target { get; set; }
        public override void Clear()
        {
            Target = null;
        }
    }
}