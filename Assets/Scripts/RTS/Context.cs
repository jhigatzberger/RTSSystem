using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    public static class Context
    {
        public static Vector3? worldPointerPosition;
        public static HashSet<BaseEntity> hoveredEntities = new HashSet<BaseEntity>();
        public static HashSet<SelectableObject> onScreen = new HashSet<SelectableObject>();
        private static readonly Selection _selection = new Selection();
        public static Selection Selection { get { return _selection; } }
    }

}
