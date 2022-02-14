using UnityEngine;
using System.Linq;

namespace JHiga.RTSEngine.Selection
{
    public class SelectorController : MonoBehaviour
    {
        private Selector[] selectors;
        private bool shouldClearSelectionOnInput = true;
        private LayerMask selectableLayerMask;
        void Awake()
        {
            selectors = GetComponents<Selector>().OrderBy(s => s.Priority).ToArray();
            selectableLayerMask = RTSWorldData.Instance.selectableLayerMask;
        }
        private void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, float.MaxValue, selectableLayerMask))
                SelectionContext.hovered = hit.collider.GetComponent<ISelectionBehaviour>().Selectable.Entity;
            else
                SelectionContext.hovered = null;
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
