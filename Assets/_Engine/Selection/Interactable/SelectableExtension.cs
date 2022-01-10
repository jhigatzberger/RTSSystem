using System;
using UnityEngine;

namespace JHiga.RTSEngine.Selection
{
    public class SelectableExtension : BaseInteractableExtension<SelectableProperties>, ISelectable
    {
        private Renderer _renderer;
        public bool Visible => _renderer.isVisible;
        public int Priority => Extendable.PlayerId == PlayerContext.PlayerId ? Properties.priority : Properties.priority + SelectionContext.NULL_PRIORITY;
        public event Action<bool> OnSelectedUpdate;
        public bool Selected => SelectionIndex>=0;
        public int SelectionIndex => _selectionIndex;
        private int _selectionIndex = -1;
        public SelectableExtension(IExtendableInteractable extendable, SelectableProperties properties) : base(extendable, properties)
        {
            SelectionBehaviour indicator = Extendable.MonoBehaviour.GetComponentInChildren<SelectionBehaviour>();
            indicator.Selectable = this;
            _renderer = indicator.gameObject.GetComponent<Renderer>();
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