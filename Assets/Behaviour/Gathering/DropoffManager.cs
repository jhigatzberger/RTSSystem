using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JHiga.RTSEngine.Gathering
{
    public static class DropoffManager
    {
        public static Dictionary<int, List<IDropoff>> resourceTypeToDropoffs = new Dictionary<int, List<IDropoff>>();
        public static IDropoff GetClosest(int resourceType, Vector3 position)
        {
            return resourceTypeToDropoffs[resourceType].OrderBy(d => Vector3.Distance(d.Entity.Position, position)).First();
        }
    }
}
