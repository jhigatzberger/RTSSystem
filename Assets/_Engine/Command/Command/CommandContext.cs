using System;
using UnityEngine;

namespace JHiga.RTSEngine.CommandPattern
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
            Debug.Log("doint it");
            OnCommandDistribute?.Invoke(command);
        }
    }

}
