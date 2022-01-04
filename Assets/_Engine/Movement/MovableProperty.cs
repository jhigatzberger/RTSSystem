using JHiga.RTSEngine;
using UnityEngine;

namespace JHiga.RTSEngine.Movement
{
    [CreateAssetMenu(fileName = "MovableProperty", menuName = "RTS/Entity/Properties/MovableProperty")]
    public class MovableProperty : ExtensionFactory
    {
        public override IExtension Build(IExtendable entity)
        {
            return new MovableExtension(entity);
        }
    }
}