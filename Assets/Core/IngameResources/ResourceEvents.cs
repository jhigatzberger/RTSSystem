
using System;

public static class ResourceEvents
{
    public static event Action<AlterResourceRequest> OnAlterResourceRequest;
    public static event Action<ResourceData> OnUpdateResource;
    public static void RequestResourceAlter(AlterResourceRequest request)
    {
        OnAlterResourceRequest?.Invoke(request);
    }
    public static void UpdateResource(ResourceData data)
    {
        OnUpdateResource?.Invoke(data);
    }
}

public struct AlterResourceRequest
{
    public int resourceType;
    public int amount;
    public Action successCallback;

    public AlterResourceRequest(ResourceData data, Action callback)
    {
        resourceType = data.resourceType;
        amount = data.amount;
        successCallback = callback;
    }

}
[Serializable]
public struct ResourceData
{
    public int resourceType;
    public int amount;
}