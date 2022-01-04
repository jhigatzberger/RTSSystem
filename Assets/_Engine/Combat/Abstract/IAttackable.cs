using JHiga.RTSEngine;

namespace JHiga.RTSEngine.Combat
{
    public interface IAttackable : IExtension
    {
        public int Health { get; set; }
        public bool IsAlive { get; }
        public void Die();
    }
}
