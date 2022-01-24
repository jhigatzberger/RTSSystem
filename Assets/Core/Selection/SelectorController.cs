using UnityEngine;
using System.Linq;

namespace JHiga.RTSEngine.Selection
{
    public class SelectorController : MonoBehaviour
    {
        private Selector[] selectors;
        private bool shouldClearSelectionOnInput = true;
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
                SelectionContext.Deselect();
        }
        public void SetClearSelectionOnInput(bool shouldClearSelectionOnInput)
        {
            this.shouldClearSelectionOnInput = shouldClearSelectionOnInput;
        }
    }
}
