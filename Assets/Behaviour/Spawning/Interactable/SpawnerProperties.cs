using System;
using UnityEngine;

namespace JHiga.RTSEngine.Spawning
{
    [CreateAssetMenu(fileName = "DefaultSpawner", menuName = "RTS/Entity/Properties/Spawner")]
    public class SpawnerProperties : ExtensionFactory
    {
        public Vector3 doorPosition;
        public override Type ExtensionType => typeof(ISpawner);
        public override IEntityExtension Build(IExtendableEntity entity)
        {
            return new SpawnerExtension(entity, this);
        }
    }
}