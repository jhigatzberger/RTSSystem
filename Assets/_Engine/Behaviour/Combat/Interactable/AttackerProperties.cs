using UnityEngine;

namespace JHiga.RTSEngine.Combat
{
    [CreateAssetMenu(fileName = "DefaultAttacker", menuName = "RTS/Entity/Properties/Attacker")]
    public class AttackerProperties : ExtensionFactory
    {
        public int damage;
        public float attackCoolDown;
        public float visionRange; 
        public float attackRange;
        public float damageDelay;
        public override IInteractableExtension Build(IExtendableInteractable extendable)
        {
            return new AttackerExtension(extendable, this);
        }
    }
}