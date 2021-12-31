using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSEngine.Core.Spawning
{
    [CreateAssetMenu(fileName = "SpawnerProperty", menuName = "RTS/Entity/Properties/SpawnerProperty")]
    public class SpawnerProperty : RTSProperty
    {
        public override IExtension Build(RTSBehaviour behaviour)
        {
            return new SpawnerExtension(behaviour);
        }
    }
}