using JHiga.RTSEngine;
using UnityEngine;

[CreateAssetMenu(fileName = "NetworkEntity", menuName = "RTS/Networking/NetworkEntity")]
public class RTSNetworkEntityFactory : EntityFactory
{
    public bool isServer;
    public bool isClient;
    public RTSNetworkComponentProperty[] properties;
    public override IExtensionFactory[] ComponentFactories => properties;
    public void Initialize()
    {
        GameObject client = new GameObject(name);
        RTSNetworkEntityBehaviour clientBehaviour = client.AddComponent<RTSNetworkEntityBehaviour>();

        if (isServer) RTSNetworkEntityBehaviour.SERVER = clientBehaviour;
        if (isClient) RTSNetworkEntityBehaviour.CLIENT = clientBehaviour;

        clientBehaviour.ScriptableComponents = Build(clientBehaviour);
    }
}
