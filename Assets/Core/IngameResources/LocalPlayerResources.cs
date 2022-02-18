using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JHiga.RTSEngine
{
    public class LocalPlayerResources : MonoBehaviour
    {
        public static LocalPlayerResources Instance;
        public int[] resources;
        private void Awake()
        {
            Instance = this;
            resources = RTSWorldData.Instance.resourceTypes.Select(r => r.startAmount).ToArray();
        }
        private void Start()
        {
            ResourceEvents.OnUpdateResource += ResourceEvents_OnUpdateResource;

        }
        public bool CanAfford(ResourceData data)
        {
            return resources[data.resourceType] >= data.amount;
        }
        private void ResourceEvents_OnUpdateResource(ResourceData obj)
        {
            Debug.Log("ResourceEvents_OnUpdateResource!");
            resources[obj.resourceType] = obj.amount;
        }
    }
}