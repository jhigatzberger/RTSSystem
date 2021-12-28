using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSEngine.Team
{
    public static class Context
    {
        public static int playerTeam;
        public static Team PlayerTeam { get => teams[playerTeam]; }
        public static Team[] teams;
        public static bool AreEnenmies(int team1, int team2)
        {
            return teams[team1].side != teams[team2].side;
        }
    }
}
