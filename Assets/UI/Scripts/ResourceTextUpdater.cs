using JHiga.RTSEngine;
using UnityEngine;
using UnityEngine.UI;

public class ResourceTextUpdater : MonoBehaviour
{
    [SerializeField] private Text[] resourceTexts;
    void Start()
    {
        for (int i = 0; i < resourceTexts.Length; i++)
            resourceTexts[i].text = LocalPlayerResources.Instance.resources[i].ToString();
        ResourceEvents.OnUpdateResource += ResourceEvents_OnUpdateResource;
    }

    private void ResourceEvents_OnUpdateResource(ResourceData obj)
    {
        resourceTexts[obj.resourceType].text = obj.amount.ToString();
    }
}
