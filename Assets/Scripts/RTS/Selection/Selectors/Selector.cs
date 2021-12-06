using UnityEngine;

namespace RTS
{
    [RequireComponent(typeof(SelectionManager))]
    public abstract class Selector : MonoBehaviour
    {
        private SelectionManager _manager;
        public abstract int Prority { get; }
        protected SelectionManager Manager
        {
            get
            {
                if (_manager == null)
                    _manager = GetComponent<SelectionManager>();
                return _manager;
            }
        }
        protected abstract bool Applicable { get; }
        public abstract void InputStart();
        public abstract void InputStop();
    }
}

