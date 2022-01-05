using JHiga.RTSEngine.InputHandling;
using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace JHiga.RTSEngine.CommandPattern
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
            CommandEvents.OnCommandDistributionRequested += DistributeCommand;
            LockStep.OnStep += LockStep_OnStep;
        }

        private void LockStep_OnStep()
        {
            while(scheduledCommands.Count > 0 && scheduledCommands.Peek().scheduledStep == LockStep.count)
                CommandEvents.RequestCommandEnqueue(scheduledCommands.Dequeue().command);
        }

        private void DistributeCommand(DistributableCommand command)
        {
            AddCommandServerRPC(command);
        }
        [ClientRpc]
        public void DistributeCommandClientRPC(ScheduledCommand command)
        {
            scheduledCommands.Enqueue(command);
        }
        [ClientRpc]
        public void AddCommandClientRPC(ulong commandID)
        {
            OnAddCommand?.Invoke(commandID, NetworkManager.LocalClientId);
        }
        [ServerRpc]
        public void ConfirmCommandServerRPC(ulong commandID, ulong clientID)
        {
            confirmationServer.Confirm(commandID, clientID);
        }
        [ServerRpc]
        public void AddCommandServerRPC(DistributableCommand command)
        {
            confirmationServer.Add(command);
        }
    }

    public class CommandConfirmationServer : NetworkBehaviour
    {
        internal Action<ScheduledCommand> OnConfirm;
        internal Action<ulong> OnAdd;

        private ulong lastCommandID = 0;
        private Dictionary<ulong, PendingCommand> pendingCommands = new Dictionary<ulong, PendingCommand>();

        public void Add(DistributableCommand command)
        {
            pendingCommands.Add(++lastCommandID, new PendingCommand
            {
                command = command,
                pendingClients = new List<ulong>(NetworkManager.Singleton.ConnectedClientsIds)
            });
            OnAdd(lastCommandID);
        }
        public void Confirm(ulong commandID, ulong clientID)
        {
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
        public DistributableCommand command;
        public List<ulong> pendingClients;

        public bool Confirm(ulong clientID)
        {
            pendingClients.Remove(clientID);
            return pendingClients.Count == 0;
        }
    }

    public struct ScheduledCommand
    {
        public DistributableCommand command;
        public ulong scheduledStep;
    }

}
