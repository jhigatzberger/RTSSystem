using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{ 
    public class SquadController : MonoBehaviour
    {
        public SelectableObject[] members;
        private List<SelectableObject> unselected;

        private void Awake()
        {
            members = GetComponentsInChildren<SelectableObject>();
            foreach (SelectableObject member in members)
                member.onSelectionChange.AddListener(UpdateSelection);
            unselected = new List<SelectableObject>(members);
        }

        private void UpdateSelection(SelectableObject selectableObject)
        {
            foreach(SelectableObject member in members)
            {
                if(member != selectableObject)
                    member.UnselectableForceSelection = selectableObject.JoinedSelection;
            }
        }
    }

}