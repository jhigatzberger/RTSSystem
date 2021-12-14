using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS.Entity.AI.Formation
{
    public static class Context
    {
        public static BaseFormation current = new SquareFormation(1.5f);
    }

}
