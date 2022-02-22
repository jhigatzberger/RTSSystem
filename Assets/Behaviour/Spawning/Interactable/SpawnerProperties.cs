using System;
using UnityEngine;

namespace JHiga.RTSEngine.Spawning
{
    [CreateAssetMenu(fileName = "DefaultSpawner", menuName = "RTS/Entity/Properties/Spawner")]
    public class SpawnerProperties : ExtensionFactory
    {
        public override Type ExtensionType => typeof(ISpawner);
        public Vector3 offset;
        public GameObject progressIndicator;
        public override IEntityExtension Build(IExtendableEntity entity)
        {
            return new SpawnerExtension(entity, this);
        }
    }
}