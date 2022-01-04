using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceTextUpdater : MonoBehaviour
{
    private Text resourceText;
    void Start()
    {
        resourceText = GetComponent<Text>();
        resourceText.text = ResourceManager.playerResources.ToString();
        ResourceManager.OnResourceUpdate += ResourceManager_OnResourceUpdate;
    }

    private void ResourceManager_OnResourceUpdate(int obj)
    {
        resourceText.text = obj.ToString();
    }
}
