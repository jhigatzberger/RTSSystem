using System;
using UnityEngine;

namespace JHiga.RTSEngine.Selection
{
    public class SelectableExtension : Extension, ISelectable
    {
        private Renderer _renderer;
        public bool Visible => _renderer.isVisible;
        private int _priority;
        public int Priority => Extendable.PlayerId == TeamContext.PlayerTeam ? _priority : _priority + SelectionContext.NULL_PRIORITY;
        public event Action<bool> OnSelectedUpdate;
        public bool Selected => SelectionIndex>=0;
        public int SelectionIndex => _selectionIndex;
        private int _selectionIndex = -1;
        public SelectableExtension(IExtendable extendable, int priority) : base(extendable)
        { 
            SelectionBehaviour indicator = Extendable.MonoBehaviour.GetComponentInChildren<SelectionBehaviour>();
            indicator.Selectable = this;
            _renderer = indicator.gameObject.GetComponent<Renderer>();
            _priority = priority;
        }
        public void Select(int index)
        {
            if(!Selected)
                OnSelectedUpdate(true);
            _selectionIndex = index;
        }
        public void Deselect()
        {
            if (Selected)
                OnSelectedUpdate(false);
            _selectionIndex = -1;
        }
        public override void Disable()
        {
            SelectionContext.Deselect(this);
        }
    }
}