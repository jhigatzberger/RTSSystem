using UnityEngine;
using RTSEngine.Core;
using RTSEngine.Team;

public class TeamMaterialExtension : RTSExtension
{
    public TeamMaterialExtension(RTSBehaviour behaviour, Texture changeTexture, Material[] teamMaterials) : base(behaviour)
    { 
        Material mat = teamMaterials[Context.PlayerTeam];
        foreach (Renderer r in Behaviour.GetComponentsInChildren<Renderer>())
            if (r.material.mainTexture == changeTexture)
                r.material = mat;
    }
}
