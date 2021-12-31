using UnityEngine;

namespace RTSEngine.Core.Combat
{
    [CreateAssetMenu(fileName = "CombatProperty", menuName = "RTS/Entity/Properties/CombatProperty")]
    public class CombatProperty : RTSProperty
    {
        public CombatStats stats;
        public override IExtension Build(RTSBehaviour behaviour)
        {
            return new CombatExtension(behaviour, stats);
        }
    }
}