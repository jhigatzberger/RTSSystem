using UnityEngine;

namespace JHiga.RTSEngine
{
    [CreateAssetMenu(fileName = "TeamMaterialsProperty", menuName = "RTS/Entity/Properties/TeamMaterialsProperty")]
    public class TeamMaterialsProperty : ExtensionFactory
    {
        public Texture changeTexture;
        public Material[] teamMaterials;
        public override IExtension Build(IExtendable entity)
        {
            return new TeamMaterialExtension(entity, changeTexture, teamMaterials);
        }
    }
}