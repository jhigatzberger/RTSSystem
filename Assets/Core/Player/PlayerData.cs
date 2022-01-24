using UnityEngine;
using System.Linq;

namespace JHiga.RTSEngine
{
    public class PlayerData : MonoBehaviour
    {
        public PlayerProperties[] players;
        private void Awake()
        {
            PlayerContext.players = players.OrderBy(t => t.id).ToArray();
            print(players.Length + " init");
        }
    }
}