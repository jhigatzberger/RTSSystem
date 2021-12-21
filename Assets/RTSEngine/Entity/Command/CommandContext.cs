using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSEngine.Entity
{
    public static class CommandContext
    {
        public static event Action<DistributedCommand> OnCommandEnqueue;
        public static event Action<DistributedCommand> OnCommandDistribute;
        public static void EnqueueCommand(DistributedCommand command)
        {
            OnCommandEnqueue?.Invoke(command);
        }
        public static void DistributeCommand(DistributedCommand command)
        {
            OnCommandDistribute?.Invoke(command);
        }
    }

}
