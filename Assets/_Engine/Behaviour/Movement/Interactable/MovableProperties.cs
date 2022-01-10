using JHiga.RTSEngine;
using UnityEngine;

namespace JHiga.RTSEngine.Movement
{
    [CreateAssetMenu(fileName = "DefaultMovable", menuName = "RTS/Entity/Properties/Movable")]
    public class MovableProperties : ExtensionFactory
    {
        public float movementSpeed;
        public override IInteractableExtension Build(IExtendableInteractable entity)
        {
            return new MovableExtension(entity, this);
        }
    }
}