using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JHiga.RTSEngine.Gather
{
    public interface IDropoff : IEntityExtension
    {
        public int[] ResourceTypes { get; }
        public void Deliver(ResourceData data);        
        public Vector3 Position { get; }
    }
}
