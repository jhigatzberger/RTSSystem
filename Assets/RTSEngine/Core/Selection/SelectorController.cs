using UnityEngine;
using System.Linq;

namespace RTSEngine.Core.Selection
{
    public class SelectorController : MonoBehaviour
    {
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
                SelectionContext.Deselect();
        }
        public static bool shouldClearSelectionOnInput = true;
        public void SetClearSelectionOnInput(bool shouldClearSelectionOnInput)
        {
            SelectorController.shouldClearSelectionOnInput = shouldClearSelectionOnInput;
        }
    }
}
