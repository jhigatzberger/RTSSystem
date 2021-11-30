using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MySelectableObject", menuName = "RTS/Selection/SelectableObject", order = 1)]
public class SelectableObjectProperties : ScriptableObject
{
    public int selectionPriority;
    public GameObject selectionIndicatorPrefab;

}
