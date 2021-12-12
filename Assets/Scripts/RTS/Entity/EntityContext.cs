using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RTS
{
    public static class EntityContext
    {
        public static HashSet<BaseEntity> hovered = new HashSet<BaseEntity>();
        public static BaseEntity FirstOrNullHovered
        {
            get
            {
                if (hovered.Count < 1)
                    return null;
                else
                    return hovered.First();
            }
        }
    }

}
