using UnityEngine;

namespace RTSEngine.Core.Movement
{
    [CreateAssetMenu(fileName = "MovableProperty", menuName = "RTS/Entity/Properties/MovableProperty")]
    public class MovableProperty : RTSProperty
    {
        public override IExtension Build(RTSBehaviour behaviour)
        {
            return new MovableExtension(behaviour);
        }
    }
}