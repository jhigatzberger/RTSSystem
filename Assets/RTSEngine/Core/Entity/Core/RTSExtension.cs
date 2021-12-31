using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSEngine.Core
{
    public abstract class RTSExtension : IExtension
    {
        public RTSBehaviour Behaviour { get; set; }
        public RTSExtension(RTSBehaviour behaviour)
        {
            Behaviour = behaviour;
        }
        public void ExitScene()
        {
            OnExitScene();
        }
        protected virtual void OnExitScene() { }
    }
}
