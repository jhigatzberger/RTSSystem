using JHiga.RTSEngine.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JHiga.RTSEngine.Construction
{
    public interface IBuildable : IEntityExtension
    {
        public GameEntityPool FinishiedEntity { get; }
        public int MaxConstructionLevel { get; }
        public int CurrentConstructionLevel { get; }
        public bool Construct(int speed);
    }
}
