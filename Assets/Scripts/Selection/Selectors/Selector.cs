using System.Collections.Generic;
using UnityEngine;

public abstract class Selector : ScriptableObject
{
    protected abstract bool Applicable { get; }
    public abstract void OnMouseDown();
    public abstract void OnMouseUp();
    public virtual void OnGUI()
    {

    }
    protected Vector2? Position { get; set; }
    public virtual void UpdatePosition(Vector2? pos)
    {
        Position = pos;
    }
}
