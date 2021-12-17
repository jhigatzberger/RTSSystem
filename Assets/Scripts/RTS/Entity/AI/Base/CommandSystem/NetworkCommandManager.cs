using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;

namespace RTS.Entity.AI
{
    public class NetworkCommandManager : NetworkBehaviour
    {
        NetworkList<DistributedCommand> PendingCommands;
        int clientsToUpdate;

        public override void OnNetworkSpawn()
        {
            if (NetworkManager.Singleton.IsServer)
                PendingCommands = new NetworkList<DistributedCommand>();
            LockStep.OnStep += ExecutePending;
            CommandContext.OnCommandEnqueue += NetworkCommandEnqueue;
            clientsToUpdate = NetworkManager.Singleton.ConnectedClients.Count;
        }

        private void NetworkCommandEnqueue(DistributedCommand command)
        {
            PendingCommands.Add(command);
        }

        private void ExecutePending(float time)
        {
            foreach (DistributedCommand command in PendingCommands)
                CommandContext.ExecuteCommand(command);
            if (--clientsToUpdate <= 0)
            {
                clientsToUpdate = NetworkManager.Singleton.ConnectedClients.Count;
                PendingCommands.Clear();
            }
        }


    }
}
