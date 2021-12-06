using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace RTS
{
    public class SelectableObject : MonoBehaviour
    {
        public ContextController controller;
        public Renderer _renderer;
        public bool Visible
        {
            get
            {
                return _renderer.isVisible;
            }
        }
        public int Priority
        {
            get
            {
                return controller.priority;
            }
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
        public static HashSet<SelectableObject> current = new HashSet<SelectableObject>();
        private SelectableObject main;
        public void Init(SelectableObject main)
        {
            this.main = main;
        }
        private void OnBecameVisible()
        {
            current.Add(main);
        }
        private void OnBecameInvisible()
        {
            current.Remove(main);
        }
    }
}