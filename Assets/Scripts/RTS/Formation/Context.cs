using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS.Formation
{
    public static class Context
    {
        public static BaseFormation current = new SquareFormation(2);
    }

}
