using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace RTS
{

    public class SelectionManager : MonoBehaviour
    {
        private Selector[] selectors;
        void Awake()
        {
            selectors = GetComponents<Selector>().OrderBy(s => s.Prority).ToArray();
        }

        #region Selection
        private Selection _selection = new Selection();
        public Selection Selection
        {
            set
            {
                if (value != null)
                    _selection = value;
                else
                    _selection = new Selection();
            }
            get
            {
                return _selection;
            }
        }
        #endregion

        #region LayerMasks
        public LayerMask groundLayerMask;
        public LayerMask selectableLayerMask;
        #endregion

        public void ClearSelection()
        {
            Selection.Clear();
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
