using System;
using UnityEngine;

namespace JHiga.RTSEngine.Combat
{
    [CreateAssetMenu(fileName = "DefaultAttackable", menuName = "RTS/Entity/Properties/Attackable")]
    public class AttackableProperties : ExtensionFactory
    {
        public int health;
        public bool hideOnDeath;
        public override Type ExtensionType => typeof(IAttackable);

        public override IEntityExtension Build(IExtendableEntity extendable)
        {
            return new AttackableExtension(extendable, this);
        }
    }
}