using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PoolingManager : MonoBehaviour
{

}

public struct Pool<T>
{
    public Stack<T> objects;
    public Pool(Func<T> create)
    {
        objects = new Stack<T>();

    }
}