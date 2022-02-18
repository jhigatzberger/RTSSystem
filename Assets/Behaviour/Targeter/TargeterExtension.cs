using System;
namespace JHiga.RTSEngine
{
    public class TargeterExtension : BaseInteractableExtension<TargeterProperties>,  ITargeter
    {
        public TargeterExtension(IExtendableEntity entity, TargeterProperties properties) : base(entity, properties)
        {
        }

        private Target? _target;

        public event Action<Target?> OnTarget;

        public Target? Target
        {
            get => _target;
            set
            {
                _target = value;
                OnTarget?.Invoke(value);
            }
        }      
    }
}