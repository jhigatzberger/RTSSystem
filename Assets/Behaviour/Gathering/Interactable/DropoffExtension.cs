using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JHiga.RTSEngine.Gathering
{
    public class DropoffExtension : BaseInteractableExtension<DropoffProperties>, IDropoff
    {
        public DropoffExtension(IExtendableEntity entity, DropoffProperties properties) : base(entity, properties)
        {
        }
        public int[] ResourceTypes => Properties.resourceTypes;

        public void Deliver(int playerId, ResourceData data)
        {
            ResourceEvents.RequestResourceAlter(new AlterResourceRequest(playerId, data, () => { Debug.Log("gathered resource"); }));
        }
        public override void Enable()
        {
            foreach (int resourceType in ResourceTypes)
            {
                if(!DropoffManager.resourceTypeToDropoffs.TryGetValue(resourceType, out List<IDropoff> dropoffs))
                {
                    dropoffs = new List<IDropoff>();
                    DropoffManager.resourceTypeToDropoffs[resourceType] = dropoffs;
                }
                dropoffs.Add(this);
            }
        }
        public override void Disable()
        {
            foreach(int resourceType in ResourceTypes)
            {
                DropoffManager.resourceTypeToDropoffs[resourceType].Remove(this);
            }
        }
    }
}