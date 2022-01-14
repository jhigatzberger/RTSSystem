using System;

public static class LockStep
{
    public static float time;
    public static ulong count;

    public static event Action OnStep;
    public static void Step(float time, ulong count)
    {
        LockStep.count = count;
        LockStep.time = time;
        OnStep?.Invoke();
    }
}
