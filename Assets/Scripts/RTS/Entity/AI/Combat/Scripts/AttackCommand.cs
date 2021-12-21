using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS.Entity.AI
{
    [CreateAssetMenu(fileName = "AttackCommand", menuName = "RTS /AI/Commands/AttackCommand")]
    public class AttackCommand : Command
    {
        public State state;
        public override bool Applicable(ICommandable entity)
        {
            BaseEntity hovered = EntityContext.FirstOrNullHovered;
            if(hovered != null && hovered.TryGetComponent(out IAttackable attackable))
                if (entity.Entity.GetComponent<IAttacker>().Team != attackable.Team)
                    return true;
            return false;
        }

        public override CommandData Build(ICommandable entity)
        {
            return new CommandData
            {
                commandID = id,
                position = EntityContext.FirstOrNullHovered.transform.position,
                targetID = EntityContext.FirstOrNullHovered.id
            };
        }

        public override void Execute(ICommandable commandable, CommandData data)
        {
            BaseEntity entity = commandable.Entity;
            entity.GetComponent<IAttacker>().Target = EntityContext.entities[data.targetID].GetComponent<IAttackable>();
            entity.GetComponent<IStateMachine>().ChangeState(state);
        }
    }

}
