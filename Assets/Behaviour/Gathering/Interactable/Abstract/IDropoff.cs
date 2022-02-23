using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JHiga.RTSEngine.Gathering
{
    public interface IDropoff : IEntityExtension
    {
        public int[] ResourceTypes { get; }
        public void Deliver(int playerId, ResourceData data);        
    }
}
