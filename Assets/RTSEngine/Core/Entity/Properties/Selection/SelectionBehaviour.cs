using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSEngine.Core.Selection
{
    [RequireComponent(typeof(Renderer))]
    public class SelectionBehaviour : MonoBehaviour
    {
        private Renderer _renderer;
        private ISelectable _selectable;
        private Color selectedColor;
        private Color unSelectedColor;
        public ISelectable Selectable
        {
            get => _selectable;
            set
            {
                if(_selectable == null)
                {
                    _selectable = value;
                    _renderer = GetComponent<Renderer>();

                    SetColor();
                    Selectable.OnSelectedUpdate += UpdateVisuals;
                }
            }
        }
        private void OnBecameVisible()
        {
            SelectionContext.onScreen.Add(Selectable);
        }
        private void OnBecameInvisible()
        {
            SelectionContext.onScreen.Remove(Selectable);
        }


        private void SetColor()
        {
            if (Selectable.Behaviour.Team == Team.TeamContext.PlayerTeam)
                selectedColor = new Color(0, 1, 0, 0.7f);
            else
                selectedColor = new Color(1, 1, 1, 0.7f);
            unSelectedColor = new Color(0, 0, 0, 0);
            UpdateVisuals(Selectable.Selected);
        }

        public void UpdateVisuals(bool selected)
        {
            _renderer.material.color = selected?selectedColor:unSelectedColor;
        }
    }
}