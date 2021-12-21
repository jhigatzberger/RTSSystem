using UnityEngine;
using System.Linq;
using RTSEngine.Entity.Selection;

namespace RTSEngine.Entity.Selection
{
    public class SelectorController : MonoBehaviour
    {
        public LayerMask selectableLayerMask;
        private Selector[] selectors;
        void Awake()
        {
            selectors = GetComponents<Selector>().OrderBy(s => s.Priority).ToArray();
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
        public void ClearSelectionOnInput()
        {
            if (shouldClearSelectionOnInput)
                Context.Deselect();
        }
        public bool shouldClearSelectionOnInput = true;
        public void SetClearSelectionOnInput(bool shouldClearSelectionOnInput)
        {
            this.shouldClearSelectionOnInput = shouldClearSelectionOnInput;
        }
    }
}
