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
            if (Instance != null || !IsOwner)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }
        #endregion

        [ClientRpc]
        public void DistributeCommandClientRPC(ScheduledCommand command)
        {
            CommandClient.Instance.Schedule(command);
        }
        [ClientRpc]
        public void AddCommandClientRPC(ulong commandID)
        {
            CommandClient.Instance.AddCommand(commandID, OwnerClientId);
        }
        [ServerRpc]
        public void ConfirmCommandServerRPC(ulong commandID, ulong clientID)
        {
             CommandServer.Instance.Confirm(commandID, clientID);
        }
        [ServerRpc]
        public void AddCommandServerRPC(SkinnedCommand command)
        {
            CommandServer.Instance.Add(command);
        }
    }
}