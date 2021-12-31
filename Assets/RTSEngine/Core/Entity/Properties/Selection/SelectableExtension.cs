using System;
using UnityEngine;

namespace RTSEngine.Core.Selection
{
    public class SelectableExtension : RTSExtension, ISelectable
    {
        private Renderer _renderer;
        public bool Visible => _renderer.isVisible;
        private int _priority;
        public int Priority => Behaviour.Team == Team.TeamContext.PlayerTeam ? _priority : _priority + SelectionContext.NULL_PRIORITY;
        public event Action<bool> OnSelectedUpdate;
        public bool Selected => SelectionIndex>=0;
        public int SelectionIndex => _selectionIndex;
        private int _selectionIndex = -1;
        public SelectableExtension(RTSBehaviour behaviour, int priority) : base(behaviour)
        { 
            SelectionBehaviour indicator = behaviour.GetComponentInChildren<SelectionBehaviour>();
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
        protected override void OnExitScene()
        {
            SelectionContext.Deselect(this);
        }
    }
}