using RTSEngine.Core;
using UnityEngine;


    [CreateAssetMenu(fileName = "TeamMaterialsProperty", menuName = "RTS/Entity/Properties/TeamMaterialsProperty")]
    public class TeamMaterialsProperty : RTSProperty
    {
        public Texture changeTexture;
        public Material[] teamMaterials;
        public override IExtension Build(RTSBehaviour behaviour)
        {
            return new TeamMaterialExtension(behaviour, changeTexture, teamMaterials);
        }
    }
