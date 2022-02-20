using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JHiga.RTSEngine.Construction
{
    public class BuilderProperties : ExtensionFactory
    {
        public int speed;
        public override Type ExtensionType => typeof(IBuilder);

        public override IEntityExtension Build(IExtendableEntity extendable)
        {
            return new BuilderExtension(extendable, this);
        }
    }
}