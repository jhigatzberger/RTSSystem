using UnityEngine;
using UnityEditor.IMGUI.Controls;
using UnityEditor;
using JHiga.RTSEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine.UIElements;
using System.Collections;
using System.Threading.Tasks;

class EntityTreeViewWindow : EditorWindow
{ 
    [SerializeField] private static TreeViewState _entityTreeViewState;
    private Button addButton;
    private Button removeButton;
    private static TreeViewState EntityTreeViewState
    {
        get
        {
            if (_entityTreeViewState == null)
                _entityTreeViewState = new TreeViewState();
            return _entityTreeViewState;
        }
    }
    public GenericTreeView TreeView
    {
        get => _treeView;
        set
        {
            _treeView = value;
        }
    }
    private GenericTreeView _treeView;
    public static EntityTreeViewWindow Instance { get; private set; }
    private Editor CacheEditor
    {
        set
        {
            InspectorContainer.Clear();
            if (value != null)
            {
                addButton.SetEnabled(!(Cache is ExtensionFactory));
                InspectorContainer.Add(value.CreateInspectorGUI());
            }
        }
    }

    private UnityEngine.Object _cache;
    private UnityEngine.Object Cache
    {
        get => _cache;
        set
        {
            if(value != _cache)
            {
                _cache = value;
                if (_cache != null)
                    CacheEditor = Editor.CreateEditor(_cache);
                else
                    CacheEditor = null;
            }
        }
    }

    private VisualElement _inspectorContainer;
    private VisualElement InspectorContainer
    {
        get
        {
            if (_inspectorContainer == null)
                InitInspector();
            return _inspectorContainer;
        }
        set
        {
            _inspectorContainer = value;
        }
    }
    private VisualElement rightHalfContainer;
    private void InitInspector()
    {
        rightHalfContainer = new VisualElement();
        rightHalfContainer.style.flexGrow = 0.5f;

        InspectorContainer = new VisualElement();
        rightHalfContainer.Add(InspectorContainer);

        rootVisualElement.style.flexDirection = FlexDirection.RowReverse;
        rootVisualElement.Add(rightHalfContainer);

        addButton = new Button(()=>Add(Cache));
        addButton.text = "Add Child";
        rightHalfContainer.Add(addButton);

        removeButton = new Button(()=>Remove(Cache, TreeView.Parent));
        removeButton.text = "Remove";
        rightHalfContainer.Add(removeButton);
    }

    void OnGUI()
    {        
        if(TreeView != null)
        {
            TreeView.OnGUI(new Rect(0, 0, position.width / 2, position.height));
            Cache = TreeView.selectedObject;
        }
    }
    private void Add(UnityEngine.Object o)
    {
        FactionProperties f = o as FactionProperties;
        if (f != null)
        {
            TypePickerWindow.Show<EntityGroup>((g) =>
            {
                f.entityGroups.Add(g);
                EditorUtility.SetDirty(g);
                AssetDatabase.SaveAssets();
                RenderTree();
            }, f);
            return;
        }
        EntityGroup g = o as EntityGroup;
        if (g != null)
        {
            TypePickerWindow.Show<GameEntityPool>((e) => {
                g.entities.Add(e);
                EditorUtility.SetDirty(g);
                AssetDatabase.SaveAssets();
                RenderTree();
            }, g);
            return;
        }
        GameEntityPool p = o as GameEntityPool;
        if (p != null)
        {
            TypePickerWindow.Show<ExtensionFactory>((f) =>
            {
                List<ExtensionFactory> l = p.properties == null ? new List<ExtensionFactory>() : p.properties.ToList();
                l.Add(f);
                p.properties = l.ToArray();
                EditorUtility.SetDirty(p);
                AssetDatabase.SaveAssets();
                RenderTree();
            },
            SOContainer.Get<ExtensionFactory>());
        }
    }
    private void Remove(UnityEngine.Object toRemove, UnityEngine.Object parent)
    {
        FactionProperties f = toRemove as FactionProperties;
        if (f != null)
        {
            Remove(f);
            return;
        }
        EntityGroup g = toRemove as EntityGroup;
        if (g != null)
        {
            Remove(g, parent);
            return;
        }
        GameEntityPool p = toRemove as GameEntityPool;
        if (p != null)
        {
            Remove(p, parent as EntityGroup);
            return;
        }
        ExtensionFactory x = toRemove as ExtensionFactory;
        if (x != null)
        {
            Remove(x, parent as GameEntityPool);
            return;
        }
    }
    private void Remove(FactionProperties faction)
    {
        foreach (EntityGroup group in new List<EntityGroup>(faction.entityGroups))
            Remove(group, faction, true);
        RemoveCleanup(faction);
    }
    private void Remove(EntityGroup group, UnityEngine.Object parent, bool cascaded = false)
    {
        if (cascaded && (TreeView.duplicateMap.TryGetValue(parent, out int value) && value > 1))
            return;
        FactionProperties f = parent as FactionProperties;
        if (f != null)
            f.entityGroups.Remove(group);
        else
        {
            EntityGroup g = parent as EntityGroup;
            g.children.Remove(group);
        }
        foreach (EntityGroup g in new List<EntityGroup>(group.children))
            Remove(g, group, true);
        foreach (GameEntityPool e in new List<GameEntityPool>(group.entities))
            Remove(e, group, true);
        RemoveCleanup(group, parent);
    }
    private void Remove(GameEntityPool gameEntityPool, EntityGroup parent, bool cascaded = false)
    {
        if (cascaded && (TreeView.duplicateMap.TryGetValue(parent, out int value) && value > 1))
            return;
        parent.entities.Remove(gameEntityPool);
        if(gameEntityPool.properties != null)
            foreach (ExtensionFactory property in new List<ExtensionFactory>(gameEntityPool.properties))
                Remove(property, gameEntityPool, true);
        RemoveCleanup(gameEntityPool, parent);
    }
    private void Remove(ExtensionFactory property, GameEntityPool parent, bool cascaded = false)
    {
        if (cascaded && (TreeView.duplicateMap.TryGetValue(parent, out int value) && value > 1))
            return;
        List<ExtensionFactory> l = new List<ExtensionFactory>(parent.properties);
        l.Remove(property);
        parent.properties = l.ToArray();
        RemoveCleanup(property, parent);
    }
    private void RemoveCleanup(UnityEngine.Object o, UnityEngine.Object parent = null)
    {
        if(parent != null)
            EditorUtility.SetDirty(parent);
        if (!TreeView.duplicateMap.TryGetValue(o, out int value) || value <= 1)
        {
            AssetDatabase.RemoveObjectFromAsset(o);
            AssetDatabase.SaveAssets();
        }
        EditorUtility.SetDirty(o);
        RenderTree();
    }
    [MenuItem("RTS/Entity Tree")]
    static void ShowWindow()
    {
        Instance = GetWindow<EntityTreeViewWindow>();
        Instance.titleContent = new GUIContent("Entity Tree");
        Instance.RenderTree();
        Instance.Show();
    }
    public void RenderTree()
    {
        GenericTreeView treeView = new GenericTreeView(EntityTreeViewState);
        treeView.RootChildren = treeView.CreateTreeViewItems(Resources.LoadAll<FactionProperties>(""),
        (faction) => treeView.CreateRecursiveReflectiveTreeViewItems<EntityGroup, GameEntityPool>(faction.entityGroups, "children", "entities",
        (pool) => treeView.CreateTreeViewItems(pool.properties)));
        TreeView = treeView;
    }
}