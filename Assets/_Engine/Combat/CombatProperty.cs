using JHiga.RTSEngine;
using UnityEngine;

namespace JHiga.RTSEngine.Combat
{
    [CreateAssetMenu(fileName = "CombatProperty", menuName = "RTS/Entity/Properties/CombatProperty")]
    public class CombatProperty : ExtensionFactory
    {
        public CombatStats stats;
        public bool canAttack;
        public override IExtension Build(IExtendable entity)
        {
            if(canAttack)
                return new CombatComponent(entity, stats);
            else
                return new AttackableComponent(entity, stats);
        }
    }
}