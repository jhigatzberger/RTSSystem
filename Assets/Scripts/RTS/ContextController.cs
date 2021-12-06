using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RTS
{   
    public class ContextController : MonoBehaviour
    {
        public int priority;
        public SelectionEvent selectionEvent = new SelectionEvent();
        private Selection _selection;
        public Selection JoinedSelection
        {
            get
            {
                return _selection;
            }
            set
            {
                if (_selection != value)
                    selectionEvent.Invoke(value);
                hasSelection = value != null;
                _selection = value;
            }
        }
        public bool hasSelection = false;
    }

    [System.Serializable]
    public class SelectionEvent : UnityEvent<Selection>
    {
    }

}
