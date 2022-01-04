using JHiga.RTSEngine;
using UnityEngine;

namespace JHiga.RTSEngine.Combat
{
    public class AttackableComponent : Extension, IAttackable
    {
        protected CombatStats stats;
        public AttackableComponent(IExtendable extendable, CombatStats stats) : base(extendable)
        {
            this.stats = stats;
            _health = stats.Health;
        }

        [SerializeField] private int _health;
        public int Health
        {
            get => _health;
            set
            {
                if (IsAlive)
                {
                    _health = value;
                    if (_health <= 0)
                        Die();
                }
            }
        }
        public bool IsAlive => Health > 0;
        public virtual void Die()
        {
            Extendable.MonoBehaviour.enabled = false;
        }

        public override void Clear()
        {
            _health = stats.Health;
        }
    }
}