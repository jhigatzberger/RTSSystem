using RTSEngine.Entity;
using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace RTSEngine.Networking
{
    public class NetworkCommandClient : NetworkBehaviour
    {
        private static Queue<ScheduledCommand> scheduledCommands = new Queue<ScheduledCommand>();
        private static CommandConfirmationServer confirmationServer;
        private static event Action<ulong, ulong> OnAddCommand;
        public override void OnNetworkSpawn()
        {
            if (!IsOwner)
                return;
            if(IsServer)
            {
                confirmationServer = gameObject.AddComponent<CommandConfirmationServer>();
                confirmationServer.OnConfirm = DistributeCommandClientRPC;
                confirmationServer.OnAdd = AddCommandClientRPC;
            }
            OnAddCommand += ConfirmCommandServerRPC;
            CommandContext.OnCommandEnqueue += CommandContext_OnCommandEnqueue;
            LockStep.OnStep += LockStep_OnStep;
        }

        private void LockStep_OnStep()
        {
            while(scheduledCommands.Count > 0 && scheduledCommands.Peek().scheduledStep == LockStep.count)
                CommandContext.DistributeCommand(scheduledCommands.Dequeue().command);
        }

        private void CommandContext_OnCommandEnqueue(DistributedCommand command)
        {
            Debug.Log("CommandContext_OnCommandEnqueue");
            AddCommandServerRPC(command);
        }
        [ClientRpc]
        public void DistributeCommandClientRPC(ScheduledCommand command)
        {
            Debug.Log("DistributeCommandClientRPC");
            scheduledCommands.Enqueue(command);
        }
        [ClientRpc]
        public void AddCommandClientRPC(ulong commandID)
        {
            Debug.Log("AddCommandClientRPC");
            OnAddCommand?.Invoke(commandID, NetworkManager.LocalClientId);
        }
        [ServerRpc]
        public void ConfirmCommandServerRPC(ulong commandID, ulong clientID)
        {
            Debug.Log("ConfirmCommandServerRPC");
            confirmationServer.Confirm(commandID, clientID);
        }
        [ServerRpc]
        public void AddCommandServerRPC(DistributedCommand command)
        {
            Debug.Log("AddCommandServerRPC");
            confirmationServer.Add(command);
        }
    }

    public class CommandConfirmationServer : NetworkBehaviour
    {
        internal Action<ScheduledCommand> OnConfirm;
        internal Action<ulong> OnAdd;

        private ulong lastCommandID = 0;
        private Dictionary<ulong, PendingCommand> pendingCommands = new Dictionary<ulong, PendingCommand>();

        public void Add(DistributedCommand command)
        {
            Debug.Log("CommandConfirmationServer Add");
            pendingCommands.Add(++lastCommandID, new PendingCommand
            {
                command = command,
                pendingClients = new List<ulong>(NetworkManager.Singleton.ConnectedClientsIds)
            });
            OnAdd(lastCommandID);
        }
        public void Confirm(ulong commandID, ulong clientID)
        {
            Debug.Log("CommandConfirmationServer Confirm");
            PendingCommand pending = pendingCommands[commandID];
            if (pending.Confirm(clientID))
            {
                OnConfirm(new ScheduledCommand{
                    command = pending.command,
                    scheduledStep = LockStep.count + 2
                });
                pendingCommands.Remove(commandID);
            }
        }
    }

    public class PendingCommand
    {
        public DistributedCommand command;
        public List<ulong> pendingClients;

        public bool Confirm(ulong clientID)
        {
            pendingClients.Remove(clientID);
            Debug.Log("PendingCommand Confirm " + pendingClients.Count);
            return pendingClients.Count == 0;
        }
    }

    public struct ScheduledCommand
    {
        public DistributedCommand command;
        public ulong scheduledStep;
    }

}
