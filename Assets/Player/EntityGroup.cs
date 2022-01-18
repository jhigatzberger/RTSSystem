using System.Collections.Generic;
using UnityEngine;

namespace JHiga.RTSEngine
{
    [CreateAssetMenu(fileName = "Group", menuName = "RTS/Group")]
    public class EntityGroup : ScriptableObject
    {
        public List<GameEntityFactory> entities = new List<GameEntityFactory>();
    }
}