using UnityEngine;

public class SelectableObject : MonoBehaviour
{
    [SerializeField] SelectableObjectProperties properties;
    GameObject selectionIndicator;
    private bool selected;
    [SerializeField] private Renderer _renderer;

    public bool ShouldShowIndicator
    {
        set
        {
            selectionIndicator.SetActive(value && SelectionManager.Priority == Priority);
        }
    }

    public int Priority
    {
        get
        {
            return properties.selectionPriority;
        }
    }

    private void Awake()
    {
        selectionIndicator = Instantiate(properties.selectionIndicatorPrefab, transform);
        selectionIndicator.SetActive(selected);
        if (_renderer == null)
            _renderer = GetComponent<Renderer>();
        Debug.Assert(_renderer != null, "Please assign a renderer to define if the object ("+gameObject.name+") is on screen (performance reasons)");
        _renderer.gameObject.AddComponent<SelectableObjectVisibilityTrigger>().Init(this);
    }

    public void Select()
    {
        SelectionManager.selection.Add(this);
        OnSelect();
    }

    public void OnSelect()
    {
        if (SelectionManager.Priority < Priority)
            SelectionManager.Priority = Priority;

        selected = true;
        ShouldShowIndicator = true;
    }

    public void Deselect()
    {
        SelectionManager.selection.Remove(this);
        OnDeselect();
    }

    public void OnDeselect()
    {
        if (SelectionManager.Priority == Priority)
            SelectionManager.Priority = -1;

        selected = false;
        ShouldShowIndicator = false;
    }

    public void UpdateVisibility(bool visible)
    {
        print(visible + gameObject.name);
        if(visible)
            SelectionManager.visibleSelectableObjects.Add(this);
        else
            SelectionManager.visibleSelectableObjects.Remove(this);
        ShouldShowIndicator = visible && selected;
    }
}

public class SelectableObjectVisibilityTrigger: MonoBehaviour
{ 
    private SelectableObject main;
    public void Init(SelectableObject main)
    {
        this.main = main;
    }
    private void OnBecameVisible()
    {
        main.UpdateVisibility(true);
    }
    private void OnBecameInvisible()
    {
        main.UpdateVisibility(false);
    }
}