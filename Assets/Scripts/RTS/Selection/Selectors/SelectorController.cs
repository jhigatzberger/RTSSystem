using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace RTS.Selectors
{
    public class SelectorController : MonoBehaviour
    {
        private Selector[] selectors;
        void Awake()
        {
            selectors = GetComponents<Selector>().OrderBy(s => s.Prority).ToArray();
        }

        public LayerMask selectableLayerMask;

        public void ClearSelectionOnInput()
        {
            if(shouldClearSelectionOnInput)
                Context.Selection.Clear();
        }

        public void BroadcastInputStart()
        {
            foreach (Selector selector in selectors)
                selector.InputStart();
        }
        public void BroadcastInputStop()
        {
            foreach (Selector selector in selectors)
                selector.InputStop();
        }

        public bool shouldClearSelectionOnInput = true;
        public void SetClearSelectionOnInput(bool shouldClearSelectionOnInput)
        {
            this.shouldClearSelectionOnInput = shouldClearSelectionOnInput;
        }
    }
}
