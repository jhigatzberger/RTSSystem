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
            Instance = this;
        }
        #endregion

        private short lastCommandID = 0;
        private readonly Dictionary<short, PendingCommand> pendingCommands = new Dictionary<short, PendingCommand>();

        public void Add(SkinnedCommand command)
        {
            pendingCommands.Add(++lastCommandID, new PendingCommand
            {
                command = command,
                pendingClients = new List<ulong>(NetworkManager.Singleton.ConnectedClientsIds)
            });
            Debug.Log("server Add clearQueueOnEnqeue" + command.references.clearQueueOnEnqeue);
            Debug.Log("server Add entities" + command.references.entities.Length);
            CommandNetwork.Instance.AddCommandClientRPC(lastCommandID);
        }
        public void Confirm(short commandID, ulong clientID)
        {
            Debug.Log("Confirm " + commandID + " client " + clientID);
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