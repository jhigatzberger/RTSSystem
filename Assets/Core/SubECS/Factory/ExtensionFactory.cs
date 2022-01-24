using System;
using UnityEngine;

namespace JHiga.RTSEngine
{
    public abstract class ExtensionFactory : ScriptableObject
    {
        public abstract Type ExtensionType { get; }
        public abstract IEntityExtension Build(IExtendableEntity extendable);
    }

}
