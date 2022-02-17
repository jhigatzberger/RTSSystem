using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace JHiga.RTSEngine.Network
{
    public class CommandServer : MonoBehaviour
    {
        public static CommandServer Instance { get; private set; }
        private short lastCommandID = 0;
        private readonly Dictionary<short, PendingCommand> pendingCommands = new Dictionary<short, PendingCommand>();
        private void Awake()
        {
            Instance = this;
        }
        public void Add(NetworkSerializableCommandData command)
        {
            pendingCommands.Add(++lastCommandID, new PendingCommand
            {
                command = command,
                pendingClients = new List<ulong>(NetworkManager.Singleton.ConnectedClientsIds)
            });
            CommandNetwork.Instance.AddCommandClientRPC(lastCommandID);
        }
        public void Confirm(short commandID, ulong clientID)
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