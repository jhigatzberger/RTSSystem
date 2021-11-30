using System.Collections.Generic;
using UnityEngine;

public abstract class Selector : Monobehaviour
{ instead of scriptable objects, these should be gameobjectes in the scene
    protected abstract bool Applicable { get; }
    public abstract void OnMouseDown();
    public abstract void OnMouseUp();
    public virtual void OnGUI() unnecessary with the new changes
    {

    }
    protected Vector2? Position { get; set; }
    public virtual void UpdatePosition(Vector2? pos)
    {
        Position = pos;
    }
}
