using System;
using System.Linq;
using Unity.Netcode;
using UnityEngine;
using RTSEngine.Entity;
using RTSEngine.Entity.Combat;

namespace RTSEngine.Networking
{
    public class SearchTargetAction : SyncedNetworkAction
    {

        public const uint ID = 0;
        internal override uint Reg_ID => ID;

        public override void ExecuteAction(int entityID)
        {
            BaseEntity entity = EntityContext.entities[entityID];
            IAttacker attacker = entity.GetComponent<IAttacker>();
            Collider[] potentialTargets = Physics.OverlapSphere(entity.transform.position, attacker.VisionRange, Team.Context.teams[entity.team].enemies);
            if (potentialTargets.Length != 0)
            {
                potentialTargets.OrderBy(c => (entity.transform.position - c.transform.position).sqrMagnitude);
                foreach (Collider c in potentialTargets)
                {
                    if (c.gameObject.TryGetComponent(out IAttackable target))
                    {
                        ((IAttacker)entity).Target = target;
                    }
                }
            }            
        }

        public override void RequestSync(int entityID)
        {
            throw new NotImplementedException();
        }
    }
}