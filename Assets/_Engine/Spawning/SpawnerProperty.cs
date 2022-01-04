using UnityEngine;

namespace JHiga.RTSEngine.Spawning
{
    [CreateAssetMenu(fileName = "SpawnerProperty", menuName = "RTS/Entity/Properties/SpawnerProperty")]
    public class SpawnerProperty : ExtensionFactory
    {
        public override IExtension Build(IExtendable entity)
        {
            return new SpawnerExtension(entity);
        }
    }
}