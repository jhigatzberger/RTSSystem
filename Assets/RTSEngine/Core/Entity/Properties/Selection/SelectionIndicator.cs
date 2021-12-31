using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSEngine.Core.Selection
{
    [RequireComponent(typeof(Renderer))]
    public class SelectionIndicator : MonoBehaviour
    {
        Renderer _renderer;
        private ISelectable _selectable;
        public ISelectable Selectable
        {
            get => _selectable;
            set
            {
                if(_selectable == null)
                {
                    _selectable = value;
                    _renderer = GetComponent<Renderer>();

                    if (!Team.Context.TeamInitialized)
                        Team.Context.OnInitializeTeam += Context_OnInitializeTeam;
                    else SetColor();
                    Selectable.OnSelectedUpdate += UpdateVisuals;
                }
            }
        }

        private void SetColor()
        {
            _renderer.material.color = Selectable.Behaviour.Team == Team.Context.PlayerTeam ? Color.green : Color.white;
        }

        private void Context_OnInitializeTeam(int obj)
        {
            Team.Context.OnInitializeTeam -= Context_OnInitializeTeam;
            SetColor();
        }

        public void UpdateVisuals(bool selected)
        {
            _renderer.enabled = selected;
        }
    }
}