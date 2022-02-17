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

            LockStep.OnStep += LockStep_OnStep;

        }
        public bool CanAfford(ResourceData data)
        {
            return resources[data.resourceType] >= data.amount;
        }
        private void LockStep_OnStep()
        {
            ResourceEvents.RequestResourceAlter(new AlterResourceRequest
            {
                amount = 1,
                resourceType = 0,
                successCallback = () => { }
            });
        }
        private void ResourceEvents_OnUpdateResource(ResourceData obj)
        {
            Debug.Log("ResourceEvents_OnUpdateResource!");
            resources[obj.resourceType] = obj.amount;
        }
    }
}