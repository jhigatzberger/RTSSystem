using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSEngine.Entity
{
    public interface IEntityExtension
    {
        public BaseEntity Entity { set;  get; }
        public void OnExitScene();
    }
}
