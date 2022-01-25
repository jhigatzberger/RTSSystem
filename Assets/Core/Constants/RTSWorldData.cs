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

        public LayerMask groundLayerMask;
        public int maxMapHight;
        public Rect mapDimensions;
    }

}
