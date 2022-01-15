namespace JHiga.RTSEngine.Combat
{
    public class AttackableExtension : BaseInteractableExtension<AttackableProperties>, IAttackable
    {
        public AttackableExtension(IExtendableEntity extendable, AttackableProperties properties) : base(extendable, properties){}
        public override void Enable()
        {
            _health = Properties.health;
        }
        private int _health;
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
            Entity.MonoBehaviour.enabled = false;
        }
    }
}