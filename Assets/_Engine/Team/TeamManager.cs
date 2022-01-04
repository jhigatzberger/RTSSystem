using UnityEngine;
using System.Linq;

namespace JHiga.RTSEngine
{
    public class TeamManager : MonoBehaviour
    {
        public TeamProperties[] teams;
        private void Awake()
        {
            TeamContext.teams = teams.OrderBy(t => t.id).ToArray();
        }
    }
}