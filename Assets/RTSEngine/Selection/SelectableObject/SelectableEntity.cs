using RTSEngine.Entity.Selection;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace RTSEngine.Entity.Selection
{
    public class SelectableEntity : MonoBehaviour, IEntityExtension
    {
        public Renderer _renderer;
        public bool Visible => _renderer.isVisible;
        public int Priority => Entity.Priority;
        public BaseEntity Entity { get; set; }
        public void OnExitScene()
        {
            enabled = false;
        }
        private void Start()
        {
            if (_renderer == null)
                _renderer = GetComponent<Renderer>();

            _renderer.gameObject.AddComponent<SelectableOnScreenObject>().Init(this);

            Debug.Assert(_renderer != null, "Please assign a renderer to define if the object (" + gameObject.name + ") is on screen (performance reasons)");
        }
    }
    public class SelectableOnScreenObject : MonoBehaviour
    {
        private SelectableEntity main;
        public void Init(SelectableEntity main)
        {
            this.main = main;
        }
        private void OnBecameVisible()
        {
            Context.onScreen.Add(main);
        }
        private void OnBecameInvisible()
        {
            Context.onScreen.Remove(main);
        }
    }
}