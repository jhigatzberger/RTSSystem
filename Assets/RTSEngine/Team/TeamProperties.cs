using UnityEngine;

namespace RTSEngine.Team
{
    [System.Serializable]
    public struct TeamProperties
    {
        public int id;
        public int side;
        public string layerName;
        public Color color;
        public LayerMask enemies;
    }
}