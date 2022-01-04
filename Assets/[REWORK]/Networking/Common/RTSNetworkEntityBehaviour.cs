using JHiga.RTSEngine;
using Unity.Netcode;
using UnityEngine;

public class RTSNetworkEntityBehaviour : NetworkBehaviour, IExtendable
{

    public static RTSNetworkEntityBehaviour CLIENT;
    public static RTSNetworkEntityBehaviour SERVER;

    #region ECS
    public IExtension[] ScriptableComponents { get; set; }
    public int EntityId => (int)NetworkManager.LocalClientId;
    public int PlayerId => (int)NetworkManager.LocalClientId;

    public MonoBehaviour MonoBehaviour => this;

    public void Clear()
    {
        foreach (IExtension component in ScriptableComponents)
            component.Clear();
    }
    public T GetScriptableComponent<T>() where T : IExtension
    {
        foreach (IExtension x in ScriptableComponents)
            if (x is T t)
                return t;
        return default;
    }
    public bool TryGetScriptableComponent<T>(out T extension) where T : IExtension
    {
        extension = default;
        foreach (IExtension x in ScriptableComponents)
            if (x is T t)
            {
                extension = t;
                return true;
            }
        return false;
    }
    #endregion
}