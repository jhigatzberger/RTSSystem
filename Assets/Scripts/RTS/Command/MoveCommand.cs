using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;

namespace RTS
{
    public class MoveCommand : Command
    {
        public Vector3 destination;
        private IMovable movable;
        public MoveCommand(ICommandable commandable, IMovable movable, Vector3 destination) : base(commandable)
        {
            this.destination = destination;
            this.movable = movable;
            Debug.Log(movable);
        }
        public override void OnExecute()
        {
            movable.MoveTo(this);
        }

        public override void OnStop()
        {
            movable.Stop();
        }
    }
}
