using UnityEngine;

namespace JHiga.RTSEngine
{
    [CreateAssetMenu(fileName = "RTSWorldData", menuName = "RTS/Singleton!/RTSWorldData")]
    public class RTSWorldData : ScriptableObject
    {
        private static RTSWorldData _instance;
        public static RTSWorldData Instance
        {
            get
            {
                if (_instance == null) _instance = Resources.Load<RTSWorldData>(Path.Generate<RTSWorldData>());
                return _instance;
            }
        }
        public ResourceType[] resourceTypes;
        public LayerMask selectableLayerMask;
        public LayerMask groundLayerMask;
        public int maxMapHight;
        public Rect mapDimensions;
        public string[] mapNames = new string[] { "GameScene" };
        public FactionProperties mapFaction;
        public FactionProperties[] playableFactions;
        public Color[] playerColors;
        public short mapTeam;
        public short[] playableTeams = new short[] { 1, 2, 3, 4, 5, 6, 7, 8 };
        public int[] teamLayers = new int[] { 10, 11, 12, 13, 14, 15, 16, 17 };
    }
}
