using RTSEngine.Entity;
using RTSEngine.Entity.Combat;
using System;
using Unity.Netcode;

namespace RTSEngine.Networking
{
    public class HealthLossAction : SyncedNetworkAction
    {
        public const uint ID = 1;
        internal override uint Reg_ID => ID;

        public override void ExecuteAction(int entityID)
        {
            IAttacker attacker = EntityContext.entities[entityID].GetComponent<IAttacker>();
            attacker.Target.Health -= attacker.Damage;
        }

        public override void RequestSync(int entityID)
        {
        }

    }
}