using UnityEngine;

namespace RTS.Entity.Selection.Selectors
{
    [RequireComponent(typeof(SelectorController))]
    public abstract class Selector : MonoBehaviour
    {
        private SelectorController _manager;
        public abstract int Priority { get; }
        protected SelectorController Manager
        {
            get
            {
                if (_manager == null)
                    _manager = GetComponent<SelectorController>();
                return _manager;
            }
        }
        protected abstract bool Applicable { get; }
        public abstract void InputStart();
        public abstract void InputStop();
    }
}

