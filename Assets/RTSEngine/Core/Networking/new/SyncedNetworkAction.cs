using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSEngine.Networking
{
    public abstract class SyncedNetworkAction : ScriptableObject
    {
        internal abstract uint Reg_ID { get; }

        public abstract void ExecuteAction(int entityID);

        public abstract void RequestSync(int entityID);
    }

}
