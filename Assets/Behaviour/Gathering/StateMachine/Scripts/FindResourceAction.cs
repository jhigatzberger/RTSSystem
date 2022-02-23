using JHiga.RTSEngine.StateMachine;
using System.Linq;
using UnityEngine;

namespace JHiga.RTSEngine.Gathering
{
    public class FindResourceAction : StateMachineAction
    {
        public override void Enter(IStateMachine stateMachine)
        {
            IExtendableEntity entity = stateMachine.Entity;
            MonoBehaviour behaviour = entity.MonoBehaviour;
            IGatherer gatherer = entity.GetExtension<IGatherer>();
            Collider[] potentialTargets = Physics.OverlapSphere(behaviour.transform.position, 25, PlayerContext.players[0].ownMask);
            if (potentialTargets.Length != 0)
            {
                potentialTargets.OrderBy(c => (behaviour.transform.position - c.transform.position).sqrMagnitude);
                foreach (Collider c in potentialTargets)
                {
                    if(c.gameObject.TryGetComponent(out IExtendableEntity cEntity))
                    {
                        if(cEntity.TryGetExtension(out IGatherable target) && target.ResourceType == gatherer.CurrentResourceType)
                        {
                            entity.GetExtension<ITargeter>().Target = new Target
                            {
                                entity = target.Entity
                            };
                            return;
                        }
                    }
                }
            }
        }
        public override void Exit(IStateMachine stateMachine)
        {
        }
    }
}
