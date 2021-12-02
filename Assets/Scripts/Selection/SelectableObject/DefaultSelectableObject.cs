using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    [CreateAssetMenu(fileName = "MySelectableObject", menuName = "RTS/Selection/SelectableObject", order = 1)]
    public class DefaultSelectableObject : ScriptableObject
    {
        public int selectionPriority;
        public GameObject selectionIndicatorPrefab;

        public virtual void Init(SelectableObject main)
        {
            main.selectionIndicator = Instantiate(selectionIndicatorPrefab, main.transform);
            main.ownPriority = selectionPriority;
        }
    }
}
