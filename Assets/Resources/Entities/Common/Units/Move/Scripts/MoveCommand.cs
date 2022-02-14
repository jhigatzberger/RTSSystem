using JHiga.RTSEngine.AI.Formation;
using JHiga.RTSEngine.InputHandling;
using JHiga.RTSEngine.Movement;
using JHiga.RTSEngine.CommandPattern;
using UnityEngine;
using JHiga.RTSEngine;
using System;

[CreateAssetMenu(fileName = "MoveCommand", menuName = "RTS/Behaviour/Commands/MoveCommand")]
public class MoveCommand : StateMachineCommandProperties
{
    public override bool Applicable(ICommandable entity)
    {
        return InputManager.worldPointerPosition.HasValue;
    }
    public override Target PackTarget(ICommandable commandable)
    {
        return new Target
        {
            position = InputManager.worldPointerPosition.Value,
        };
    }
    protected override void BeforeStateChange(ICommandable commandable, ResolvedCommandReferences references)
    {
        commandable.Entity.GetExtension<IMovable>().Enqueue(new Target { position = FormationContext.current.GetPosition(references.target.position, commandable.Entity, Array.IndexOf(references.entities, commandable.Entity), references.entities.Length) });
    }
}

