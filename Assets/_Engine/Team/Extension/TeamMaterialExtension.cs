using UnityEngine;

namespace JHiga.RTSEngine
{
    public class TeamMaterialExtension : Extension
    {
        public TeamMaterialExtension(IExtendable extendable, Texture changeTexture, Material[] teamMaterials) : base(extendable)
        {
            Material mat = teamMaterials[TeamContext.PlayerTeam];
            foreach (Renderer r in Extendable.MonoBehaviour.GetComponentsInChildren<Renderer>())
                if (r.material.mainTexture == changeTexture)
                    r.material = mat;
        }

    }
}