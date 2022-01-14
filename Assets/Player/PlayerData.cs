using UnityEngine;
using System.Linq;

namespace JHiga.RTSEngine
{
    public class PlayerData : MonoBehaviour
    {
        public const int MAX_PLAYERS = 8;
        public PlayerProperties[] players;
        private void Awake()
        {
            PlayerContext.players = players.OrderBy(t => t.id).ToArray();
            print(players.Length + " init");
        }
    }
}