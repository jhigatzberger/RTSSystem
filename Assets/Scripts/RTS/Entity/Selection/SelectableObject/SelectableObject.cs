using RTS.Entity.Selection;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace RTS.Entity.Selection
{
    public class SelectableObject : MonoBehaviour, IEntityExtension
    {
        public BaseEntity controller;
        public Renderer _renderer;
        public bool Visible => _renderer.isVisible;
        public int Priority => controller.Priority;

        public BaseEntity Entity => controller;

        public void OnExitScene()
        {
            enabled = false;
        }

        private void Awake()
        {
            if (_renderer == null)
                _renderer = GetComponent<Renderer>();

            _renderer.gameObject.AddComponent<SelectableOnScreenObject>().Init(this);

            Debug.Assert(_renderer != null, "Please assign a renderer to define if the object (" + gameObject.name + ") is on screen (performance reasons)");
        }
    }
    public class SelectableOnScreenObject : MonoBehaviour
    {
        private SelectableObject main;
        public void Init(SelectableObject main)
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