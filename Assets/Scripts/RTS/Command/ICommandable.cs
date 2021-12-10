using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    public interface ICommandable
    {
        public Command Current { get; set; }
        public void Enqueue(Command command);
        public void ExecuteFirst();
        public void StopAndClear();
        public Type[] AvailableCommands();
        public Command CreateCommandFromContext();
    }
}

