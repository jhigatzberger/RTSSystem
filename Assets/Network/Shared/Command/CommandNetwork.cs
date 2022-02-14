using JHiga.RTSEngine.CommandPattern;
using Unity.Netcode;

namespace JHiga.RTSEngine.Network
{
    public class CommandNetwork : NetworkBehaviour
    {
        #region Singleton
        public static CommandNetwork Instance { get; private set; }
        private void Awake()
        {
            Instance = this;
        }
        #endregion

        [ClientRpc]
        public void DistributeCommandClientRPC(ScheduledCommand command)
        {
            CommandClient.Instance.Schedule(command);
        }
        [ClientRpc]
        public void AddCommandClientRPC(short commandID)
        {
            CommandClient.Instance.AddCommand(commandID, NetworkManager.LocalClientId);
        }
        [ServerRpc(RequireOwnership = false)]
        public void ConfirmCommandServerRPC(short commandID, ulong clientID)
        {
             CommandServer.Instance.Confirm(commandID, clientID);
        }
        [ServerRpc(RequireOwnership = false)]
        public void AddCommandServerRPC(NetworkSerializableCommandData command)
        {
            CommandServer.Instance.Add(command);
        }
    }
}