using System;
using UnityEngine;

namespace JHiga.RTSEngine
{
    [CreateAssetMenu(fileName = "DefaultPlayerMaterials", menuName = "RTS/Entity/Properties/PlayerMaterials")]
    public class PlayerMaterialsProperties : ExtensionFactory
    {
        public Texture changeTexture;
        public Material[] playerMaterials;

        public override Type ExtensionType => typeof(PlayerMaterialExtension);

        public override IEntityExtension Build(IExtendableEntity entity)
        {
            return new PlayerMaterialExtension(entity, this);
        }
    }
}