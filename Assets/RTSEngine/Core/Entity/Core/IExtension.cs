using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSEngine.Core
{
    public interface IExtension
    {
        public RTSBehaviour Behaviour { set;  get; }
        public void ExitScene();
    }
}
