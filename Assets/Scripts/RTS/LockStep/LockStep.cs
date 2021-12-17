using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class LockStep
{

    public static event Action<float> OnStep;
    public static void Step(float step)
    {
        OnStep?.Invoke(step);
    }

}
