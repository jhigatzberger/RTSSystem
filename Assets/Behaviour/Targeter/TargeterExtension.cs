using System;
namespace JHiga.RTSEngine
{
    public class TargeterExtension : BaseInteractableExtension<TargeterProperties>,  ITargeter
    {
        public TargeterExtension(IExtendableEntity entity, TargeterProperties properties) : base(entity, properties)
        {
        }
        private Target? _target;
        public Target? Target { get => _target; set { _target = value; OnTargetEvent?.Invoke(value); } }

        public event Action<Target?> OnTargetEvent;
        public override void Clear()
        {
            Target = null;
        }
    }
}