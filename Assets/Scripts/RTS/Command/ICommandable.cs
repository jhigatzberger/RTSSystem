using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS.Command
{
    public interface ICommandable
    {
        public BaseCommand Current { get; set; }
        public void Enqueue(BaseCommand command);
        public void ExecuteFirst();
        public void StopAndClear();
        public Type[] AvailableCommands();
        public BaseCommand CreateCommandFromContext();
    }
}

