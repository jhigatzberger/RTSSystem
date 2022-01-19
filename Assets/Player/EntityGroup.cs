using System.Collections.Generic;
using UnityEngine;

namespace JHiga.RTSEngine
{
    [CreateAssetMenu(fileName = "Group", menuName = "RTS/Group")]
    public class EntityGroup : ScriptableObject
    {
        public List<GameEntityPool> entities = new List<GameEntityPool>();
        public List<EntityGroup> children = new List<EntityGroup>();

        public List<GameEntityPool> GetAllEntities()
        {
            List<GameEntityPool> allEntities = new List<GameEntityPool>(entities);
            foreach (EntityGroup child in children)
                allEntities.AddRange(child.GetAllEntities());
            return allEntities;
        }
    }
}