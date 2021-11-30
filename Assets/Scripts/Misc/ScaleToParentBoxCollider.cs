using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleToParentBoxCollider : MonoBehaviour
{
    void Awake()
    {
        BoxCollider collider = transform.parent.GetComponent<BoxCollider>();
        Vector3 rotatedScale = collider.size;
        float height = rotatedScale.y, width = rotatedScale.x > rotatedScale.z ? rotatedScale.x : rotatedScale.z;
        rotatedScale.z = height;
        rotatedScale.y = width;
        rotatedScale.x = width;
        transform.localScale = rotatedScale * 2;
    }
}
