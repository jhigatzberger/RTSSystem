using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS.Entity.AI
{
    public static class CommandContext
    {
        public static event Action<DistributedCommand> OnCommandEnqueue;
        public static event Action<DistributedCommand> OnCommandExecute;
        public static void EnqueueCommand(DistributedCommand command)
        {
            OnCommandEnqueue?.Invoke(command);
        }
        public static void ExecuteCommand(DistributedCommand command)
        {
            OnCommandExecute?.Invoke(command);
        }
    }

}
