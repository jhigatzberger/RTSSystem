using JHiga.RTSEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JHiga.RTSEngine.Construction
{
    public class BuildableProperties : ExtensionFactory
    {
        public override Type ExtensionType => typeof(IBuildable);
        public GameEntityPool finishiedEntity;
        public int maxConstructionLevel;
        public override IEntityExtension Build(IExtendableEntity extendable)
        {
            return new BuildableExtension(extendable, this);
        }
    }
}