using JHiga.RTSEngine.CommandPattern;
using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace JHiga.RTSEngine.Network
{
    public class CommandServer : MonoBehaviour
    {
        #region Singleton
        public static CommandServer Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }
        #endregion

        private ulong lastCommandID = 0;
        private readonly Dictionary<ulong, PendingCommand> pendingCommands = new Dictionary<ulong, PendingCommand>();

        public void Add(SkinnedCommand command)
        {
            pendingCommands.Add(++lastCommandID, new PendingCommand
            {
                command = command,
                pendingClients = new List<ulong>(NetworkManager.Singleton.ConnectedClientsIds)
            });
            CommandNetwork.Instance.AddCommandClientRPC(lastCommandID);
        }
        public void Confirm(ulong commandID, ulong clientID)
        {
            PendingCommand pending = pendingCommands[commandID];
            if (pending.Confirm(clientID))
            {
                CommandNetwork.Instance.DistributeCommandClientRPC(new ScheduledCommand
                {
                    command = pending.command,
                    scheduledStep = LockStep.count + 2
                });
                pendingCommands.Remove(commandID);
            }
        }
    }
}