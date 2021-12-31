using UnityEngine;

namespace RTSEngine.Core.Combat
{
    [CreateAssetMenu(fileName = "CombatStats", menuName = "RTS/Stats/CombatStats")]
    public class CombatStats : ScriptableObject
    {
        public int Health;
        public int Damage;
        public float DamageDelay;
        public float AttackCooldown;
        public float VisionRange;
        public float AttackRange;
    }

}
