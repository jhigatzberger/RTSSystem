using JHiga.RTSEngine;
using UnityEngine;

public abstract class RTSNetworkComponentProperty : ScriptableObject, IExtensionFactory
{
    public abstract IExtension Build(IExtendable extendable);
}
