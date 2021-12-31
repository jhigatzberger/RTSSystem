using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSEngine.Team
{
    public static class TeamContext
    {
        public static bool TeamInitialized { get => PlayerTeam >= 0; }
        public static event Action<int> OnInitializeTeam;

        private static int _playerTeam = -1;
        public static int PlayerTeam
        {
            get => _playerTeam;
            set
            {
                if (value != _playerTeam)
                {
                    _playerTeam = value;
                    OnInitializeTeam?.Invoke(value);
                }
            }
        }
        public static TeamProperties PlayerTeamData { get => teams[PlayerTeam]; }
        public static TeamProperties[] teams;
        public static bool AreEnenmies(int team1, int team2)
        {
            return teams[team1].side != teams[team2].side;
        }
    }
}
