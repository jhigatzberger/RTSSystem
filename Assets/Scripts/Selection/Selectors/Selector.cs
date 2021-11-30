using UnityEngine;

[RequireComponent(typeof(SelectionManager))]
public abstract class Selector : MonoBehaviour
{
    private SelectionManager _manager;
    protected SelectionManager Manager
    {
        get
        {
            if (_manager == null)
                _manager = GetComponent<SelectionManager>();
            return _manager;
        }
    }
    protected abstract bool Applicable { get; }
    public abstract void Down();
    public abstract void Up();
}
