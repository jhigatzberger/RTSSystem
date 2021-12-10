using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace RTS
{
    public class SelectorController : MonoBehaviour
    {
        private Selector[] selectors;
        void Awake()
        {
            selectors = GetComponents<Selector>().OrderBy(s => s.Prority).ToArray();
        }

        public LayerMask selectableLayerMask;

        public void ClearSelection()
        {
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
    }
}
