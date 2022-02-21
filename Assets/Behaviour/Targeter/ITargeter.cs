using System;

namespace JHiga.RTSEngine
{
    public interface ITargeter : IEntityExtension
    {
        public event Action<Target?> OnTargetEvent;
        public Target? Target { get; set; }
    }
}
