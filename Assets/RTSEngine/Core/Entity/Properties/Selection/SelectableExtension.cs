using System;
using UnityEngine;

namespace RTSEngine.Core.Selection
{
    public class SelectableExtension : RTSExtension, ISelectable
    {
        public SelectableExtension(RTSBehaviour behaviour, int priority, string rendererTag) : base(behaviour)
        {
            foreach (Transform tr in behaviour.transform.parent)
                if (tr.CompareTag(rendererTag))
                    _renderer = tr.GetComponent<Renderer>();
            behaviour.GetComponentInChildren<SelectionIndicator>().Selectable = this;
            _renderer.gameObject.AddComponent<SelectableOnScreenObject>().Init(this);
            _priority = priority;
        }


        public Renderer _renderer;
        public bool Visible => _renderer.isVisible;

        private int _priority;
        public int Priority => Behaviour.Team == Team.Context.PlayerTeam ? _priority : _priority + SelectionContext.NULL_PRIORITY;

        public event Action<bool> OnSelectedUpdate;

        public bool Selected => SelectionIndex>=0;

        public int SelectionIndex => _selectionIndex;

        private int _selectionIndex = -1;
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