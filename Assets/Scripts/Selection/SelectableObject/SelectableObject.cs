using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SelectableObject : MonoBehaviour
{
    public static HashSet<SelectableObject> onScreen = new HashSet<SelectableObject>();
    public SelectableObjectProperties properties;
    public Renderer _renderer;

    public GameObject selectionIndicator;


    public static int selectedPriority = -1;
    public int ownPriority;
    public bool selected;

    private void Awake()
    {
        if (properties != null)
        {
            selectionIndicator = Instantiate(properties.selectionIndicatorPrefab, transform);
            ownPriority = properties.selectionPriority;
        }

        Debug.Assert(selectionIndicator != null, "Please make sure " + gameObject.name + " has a selection indicator object");
        selectionIndicator.SetActive(selected);

        if (_renderer == null)
            _renderer = GetComponent<Renderer>();

        Debug.Assert(_renderer != null, "Please assign a renderer to define if the object ("+gameObject.name+") is on screen (performance reasons)");
        _renderer.gameObject.AddComponent<ScreenObjectUpdater>().Init(this);

    }
    public void UpdateIndicator(bool show)
    {
        selectionIndicator.SetActive(show && selectedPriority == ownPriority);
    }
    public void OnSelect()
    {
        UpdateIndicator(selected = true);
    }
 
    public void OnDeselect()
    {
        UpdateIndicator(selected = false);
    }

    public void UpdateVisibility(bool visible)
    {
        print(visible + gameObject.name);
        if(visible)
            onScreen.Add(this);
        else
            onScreen.Remove(this);
        UpdateIndicator(visible && selected);
    }
}

public class ScreenObjectUpdater: MonoBehaviour
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

[CustomEditor(typeof(SelectableObject))]
public class SelectableObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SelectableObject selectableObject = target as SelectableObject;
        selectableObject.properties = (SelectableObjectProperties)EditorGUILayout.ObjectField("Properties", selectableObject.properties, typeof(SelectableObjectProperties), false);
        if (selectableObject.properties == null)
        {
            selectableObject.selectionIndicator = (GameObject)EditorGUILayout.ObjectField("Selection Indicator", selectableObject.selectionIndicator, typeof(GameObject), true);
            selectableObject.ownPriority = EditorGUILayout.IntField("Priority", 0);
        }
        Renderer ownRenderer = selectableObject.GetComponent<Renderer>();
        if (ownRenderer == null)
            selectableObject._renderer = (Renderer)EditorGUILayout.ObjectField("Renderer", selectableObject._renderer, typeof(Renderer), true);
        else
            selectableObject._renderer = ownRenderer;
    }
}