using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAndEnable : MonoBehaviour
{
    public List<GameObject> toEnable;
    void Start()
    {
        Invoke("ExecuteDestroyAndEnable", 10);
    }

    void ExecuteDestroyAndEnable()
    {
        foreach (GameObject go in toEnable)
            go.SetActive(true);
        Destroy(gameObject);
    }
}
