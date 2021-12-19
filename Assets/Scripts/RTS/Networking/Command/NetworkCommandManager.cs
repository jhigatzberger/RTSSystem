using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace RTS.Entity
{
    public class NetworkCommandManager : NetworkBehaviour
    {
        NetworkList<DistributedCommand> DistributedCommands;
        private List<DistributedCommand> pendingCommands = new List<DistributedCommand>();

        public override void OnNetworkSpawn()
        {
            CommandContext.OnCommandEnqueue += EnqueueCommandServerRPC;
            if (IsServer)
                LockStep.OnStep += DistributePending;
            DistributedCommands.OnListChanged += DistributedCommands_OnListChanged;
        }

        private void DistributedCommands_OnListChanged(NetworkListEvent<DistributedCommand> changeEvent)
        {
            if (changeEvent.Type == NetworkListEvent<DistributedCommand>.EventType.Add)
                CommandContext.DistributeCommand(changeEvent.Value);
        }

        private void OnDisable()
        {
            LockStep.OnStep -= DistributePending;
            CommandContext.OnCommandEnqueue -= EnqueueCommandServerRPC;
        }

        [ServerRpc]
        private void EnqueueCommandServerRPC(DistributedCommand command)
        {
            pendingCommands.Add(command);
        }

        private void DistributePending(float time)
        {
            foreach (DistributedCommand command in pendingCommands)
                DistributedCommands.Add(command);
            pendingCommands.Clear();
        }
    }
}
