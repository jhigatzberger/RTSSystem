using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameStart : MonoBehaviour
{
    public static event Action OnStart;
    public static void StartGame()
    {
        print("game start");
        OnStart?.Invoke();
    }
}
