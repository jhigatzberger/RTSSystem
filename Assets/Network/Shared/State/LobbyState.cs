using Unity.Netcode;
using UnityEngine.SceneManagement;

namespace JHiga.RTSEngine.Network
{
    public class LobbyState : NetworkState
    {
        public static LobbyState Instance { get; private set; }

        public override State Type => State.Lobby;

        private void Start()
        {
            Instance = this;
            SceneManager.LoadScene(1);
        }
        public void ChooseFaction(short faction)
        {
            ChooseFactionServerRpc(faction, NetworkManager.LocalClientId);
        }
        [ServerRpc(RequireOwnership = false)]
        private void ChooseFactionServerRpc(short faction, ulong clientId)
        {
            var playerData = NetworkGameManager.Instance.playerData;
            for (int i = 0; i < playerData.Count; i++)
            {
                if (playerData[i].clientId == clientId)
                    playerData[i] = PlayerState.Update(playerData[i], faction, PlayerState.ReplaceType.FactionId);
            }
        }
        public void ChooseTeam(short value)
        {
            ChooseTeamServerRpc(value, NetworkManager.LocalClientId);
        }
        [ServerRpc(RequireOwnership = false)]
        private void ChooseTeamServerRpc(short team, ulong clientId)
        {
            var playerData = NetworkGameManager.Instance.playerData;
            for (int i = 0; i < playerData.Count; i++)
            {
                if (playerData[i].clientId == clientId)
                    playerData[i] = PlayerState.Update(playerData[i], team, PlayerState.ReplaceType.Team);
            }
        }
    }
}