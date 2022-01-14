using JHiga.RTSEngine;

namespace JHiga.RTSEngine.Combat
{
    /// <summary>
    /// Handles combat with an <see cref="IAttackable"/>
    /// </summary>
    public interface IAttacker: IInteractableExtension
    {        
        /// <summary>
        /// The current <see cref="IAttackable"/> target.
        /// </summary>        
        public IAttackable Target { get; set; }

        /// <summary>
        /// Checks whether the distance to <see cref="Target"/> is smaller or equal to <see cref="AttackRange"/>.
        /// You can enter an Attack <see cref="State"/> with the <see cref="InRangeDecision"/>.
        /// </summary>
        public bool IsInRange { get; }

        /// <summary>
        /// Needed for automatic <see cref="Target"/> finding.
        /// Comes from an <see cref="CombatStats"/> instance.
        /// </summary>
        public float VisionRange { get; }

        /// <summary>
        /// Needed for automatic <see cref="IsInRange"/> finding.
        /// Comes from an <see cref="CombatStats"/> instance.
        /// </summary>
        public float AttackRange { get; }

        /// <summary>
        /// To be subtracted from <see cref="IAttackable.Health"/> when performing an <see cref="Attack"/>.
        /// Comes from an <see cref="CombatStats"/> instance.
        /// </summary>
        public int Damage { get; }
        
        /// <summary>
        /// Cooldown in seconds after applying the Damage from an <see cref="Attack"/> after the <see cref="DamageDelay"/> until <see cref="CanAttack"/> is true again.
        /// Comes from a <see cref="CombatStats"/> instance.
        /// </summary>
        public float AttackCooldown { get; }

        /// <summary>
        /// Checks whether we have a <see cref="Target"/> and the time of the last <see cref="Attack"/> was longer ago than <see cref="DamageDelay"/> and <see cref="IsInRange"/> is true for the <see cref="Target"/>
        /// </summary>
        public bool CanAttack { get; }

        /// <summary>
        /// The time in seconds between subtracting the <see cref="Damage"/> from the <see cref="IAttackable.Health"/> of <see cref="Target"/>.
        /// </summary>
        public float DamageDelay { get; }

        /// <summary>
        /// True for the <see cref="DamageDelay"/> duration after performing an <see cref="Attack"/>.
        /// </summary>
        public bool IsAttacking { get; }
        
        /// <summary>
        /// Subtracts the <see cref="Damage"/> from the <see cref="Target"/> <see cref="IAttackable.Health"/>.
        /// Triggers an attack animation.
        /// </summary>
        public void Attack();
    }
}
