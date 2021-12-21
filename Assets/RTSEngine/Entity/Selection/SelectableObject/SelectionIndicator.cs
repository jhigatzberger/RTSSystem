using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSEngine.Entity.Selection
{
    [RequireComponent(typeof(Renderer))]
    public class SelectionIndicator : MonoBehaviour
    {
        [SerializeField] BaseEntity controller;
        Renderer _renderer;
        void Awake()
        {
            _renderer = GetComponent<Renderer>();
            controller.OnSelectedUpdate += UpdateVisuals;
        }

        public void UpdateVisuals(bool selected)
        {
            _renderer.enabled = selected;
        }
    }
}