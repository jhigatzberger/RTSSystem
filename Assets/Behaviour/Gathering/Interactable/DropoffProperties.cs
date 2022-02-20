using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JHiga.RTSEngine.Gathering
{
    public class DropoffProperties : ExtensionFactory
    {
        public override Type ExtensionType => typeof(IDropoff);
        public int[] resourceTypes;
        public Vector3 offset;
        public override IEntityExtension Build(IExtendableEntity extendable)
        {
            return new DropoffExtension(extendable, this);
        }
    }
}