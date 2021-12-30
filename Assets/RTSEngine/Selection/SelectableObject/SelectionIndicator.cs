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
        void Start()
        {
            _renderer = GetComponent<Renderer>();

            if(!Team.Context.TeamInitialized)
                Team.Context.OnInitializeTeam += Context_OnInitializeTeam;
            else
                _renderer.material.color = controller.Team == Team.Context.PlayerTeam ? Color.green : Color.white;
            controller.OnSelectedUpdate += UpdateVisuals;
        }

        private void Context_OnInitializeTeam(int obj)
        {
            _renderer.material.color = controller.Team == Team.Context.PlayerTeam ? Color.green : Color.white;
            Team.Context.OnInitializeTeam -= Context_OnInitializeTeam;
        }

        public void UpdateVisuals(bool selected)
        {
            _renderer.enabled = selected;
        }
    }
}