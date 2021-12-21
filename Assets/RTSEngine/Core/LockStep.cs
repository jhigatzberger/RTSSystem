using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class LockStep
{
    public static float time;

    public static event Action OnStep;
    public static void Step(float step)
    {
        OnStep?.Invoke();
        time = step;
    }

}
