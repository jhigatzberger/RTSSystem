using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamMaterialManager : MonoBehaviour
{
    public static Dictionary<int, Material> teamMaterials = new Dictionary<int, Material>();

    public Material[] _teamMaterials;

    private void Start()
    {
        for (int i = 0; i < _teamMaterials.Length; i++)
            teamMaterials.Add(i, _teamMaterials[i]);
    }

}
