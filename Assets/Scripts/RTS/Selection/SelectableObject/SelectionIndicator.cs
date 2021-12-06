using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    [RequireComponent(typeof(Renderer))]
    public class SelectionIndicator : MonoBehaviour
    {
        [SerializeField] ContextController controller;
        Renderer _renderer;
        void Awake()
        {
            _renderer = GetComponent<Renderer>();
            controller.selectionEvent.AddListener(UpdateVisuals);
        }

        public void UpdateVisuals(Selection selection)
        {
            _renderer.enabled = selection != null;
        }
    }
}