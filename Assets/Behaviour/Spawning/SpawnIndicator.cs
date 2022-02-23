using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIndicator : MonoBehaviour
{
    [SerializeField] private Renderer rend;
    [SerializeField] private Material allowBuildMat;
    [SerializeField] private Material forbidBuildMat;
    [SerializeField] LayerMask layerMask;
    public bool AllowSpawn => Touches == 0;
    private int _touches;
    private int Touches
    {
        get => _touches;
        set
        {
            if(value > 0)
                rend.material = forbidBuildMat;
            else
                rend.material = allowBuildMat;
            _touches = value;

        }
    }

    private void Awake()
    {
        Touches = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(layerMask == (layerMask | (1 << other.gameObject.layer)))
        {
            Debug.Log(Touches + " enter");
            Touches++;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (layerMask == (layerMask | (1 << other.gameObject.layer)))
        {
            Debug.Log(Touches + " exit");
            Touches--;
        }
    }
}
