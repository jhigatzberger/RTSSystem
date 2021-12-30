using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSEngine.Team
{
    [System.Serializable]
    public struct Team
    {
        public int id;
        public int side;
        public int layer;
        public Color color;
        public LayerMask enemies;
    }
}