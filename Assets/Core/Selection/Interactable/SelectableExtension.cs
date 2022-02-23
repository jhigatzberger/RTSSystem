using System;
using UnityEngine;

namespace JHiga.RTSEngine.Selection
{
    public class SelectableExtension : BaseInteractableExtension<SelectableProperties>, ISelectable
    {
        private Renderer _renderer;
        public bool Visible => _renderer.isVisible;
        public int Priority => Entity.UniqueID.playerIndex == PlayerContext.PlayerId ? Properties.priority : Properties.priority + SelectionContext.NULL_PRIORITY;
        public event Action<bool> OnSelectedUpdate;
        public bool Selected => SelectionIndex>=0;
        public int SelectionIndex => _selectionIndex;
        private int _selectionIndex = -1;
        private SelectionBehaviour indicator;
        public SelectableExtension(IExtendableEntity extendable, SelectableProperties properties) : base(extendable, properties)
        {
            indicator = UnityEngine.Object.Instantiate(properties.selectionBehaviourPrefab, Entity.MonoBehaviour.transform).GetComponent<SelectionBehaviour>();
            indicator.gameObject.transform.localScale *= Properties.scale;
            indicator.Selectable = this;
            _renderer = indicator.gameObject.GetComponent<Renderer>();
        }
        public void Select(int index)
        {
            if(!Selected)
                OnSelectedUpdate(true);
            _selectionIndex = index;
            if(Properties.actions != null && index == 0 && Entity.IsActivePlayer)
            {
                foreach (BaseAction action in Properties.actions)
                    action.Run(Entity);
            }
        }
        public void Deselect()
        {
            if (Selected)
                OnSelectedUpdate(false);
            if (Properties.actions != null && _selectionIndex == 0 && Entity.IsActivePlayer)
            {
                foreach (BaseAction action in Properties.actions)
                    action.Stop(Entity);
            }
            _selectionIndex = -1;
        }

        public override void Enable()
        {
            indicator.enabled = true;
        }

        public override void Disable()
        {
            SelectionContext.Deselect(this);
            indicator.enabled = false;
        }
    }
}