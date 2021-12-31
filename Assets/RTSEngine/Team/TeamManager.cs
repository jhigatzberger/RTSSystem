using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace RTSEngine.Team
{
    public class TeamManager : MonoBehaviour
    {
        public TeamProperties[] teams;
        private void Awake()
        {
            Context.teams = teams.OrderBy(t => t.id).ToArray();
        }
    }
}