using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class GameStart
{
    public static event Action OnStart;
    public static void StartGame()
    {
        OnStart?.Invoke();
    }
}
