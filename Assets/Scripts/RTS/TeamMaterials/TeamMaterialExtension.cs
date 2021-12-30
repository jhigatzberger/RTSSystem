using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTSEngine.Entity;
public class TeamMaterialExtension : MonoBehaviour, IEntityExtension
{
    public BaseEntity Entity { get; set; }
    public Renderer[] renders;

    public void OnExitScene()
    {       
    }

    private void Start()
    {
        Material mat = TeamMaterialManager.teamMaterials[Entity.Team];
        foreach (Renderer r in renders)
            r.material = mat;
    }
}
