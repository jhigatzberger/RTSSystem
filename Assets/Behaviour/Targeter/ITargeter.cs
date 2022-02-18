using System;

namespace JHiga.RTSEngine
{
    public interface ITargeter : IEntityExtension
    {
        public Target? Target { get; set; }
        public event Action<Target?> OnTarget;
    }
}
