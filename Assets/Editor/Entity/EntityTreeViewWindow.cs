using UnityEngine;
using UnityEditor.IMGUI.Controls;
using UnityEditor;
using JHiga.RTSEngine;
using System.Collections.Generic;
using System.Linq;
using System;

class EntityTreeViewWindow : EditorWindow
{ 
    [SerializeField] private static TreeViewState _entityTreeViewState;
    private static TreeViewState EntityTreeViewState
    {
        get
        {
            if (_entityTreeViewState == null)
                _entityTreeViewState = new TreeViewState();
            return _entityTreeViewState;
        }
    }
    public GenericTreeView treeView;
    public static EntityTreeViewWindow Instance { get; private set; }
    private Editor cacheEditor;
    private UnityEngine.Object cache;
    void OnGUI()
    {
        treeView.OnGUI(new Rect(0, 0, position.width/2, position.height));        
        if(treeView.selectedObject != null && cache != treeView.selectedObject)
        {
            cache = treeView.selectedObject;
            cacheEditor = Editor.CreateEditor(cache);
        }
        GUILayout.BeginHorizontal();
        GUILayout.Space(position.width / 2);
        GUILayout.BeginVertical();
        if (cacheEditor != null)
        {
            cacheEditor.OnInspectorGUI();
            EntityGroup selectedGroup = treeView.selectedObject as EntityGroup;
            if (selectedGroup != null)
                if (GUILayout.Button("Add child group"))
                    GenericCreationWindow.Show<EntityGroup>((g) => selectedGroup.children.Add((EntityGroup)g), selectedGroup);
            if (!(treeView.selectedObject is ExtensionFactory) && GUILayout.Button("Add to " + cache.name))
                Add(treeView.selectedObject);
            if (GUILayout.Button("Remove " + cache.name))
                Remove(treeView.selectedObject, treeView.GetParent());
        }
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
    }
    private void Add(UnityEngine.Object o)
    {
        FactionProperties f = o as FactionProperties;
        if (f != null)
        {
            GenericCreationWindow.Show<EntityGroup>((g) => f.entityGroups.Add((EntityGroup)g), f);
            return;
        }
        EntityGroup g = o as EntityGroup;
        if (g != null)
        {
            GenericCreationWindow.Show<GameEntityPool>((e) => g.entities.Add((GameEntityPool)e), g);
            return;
        }
        GameEntityPool p = o as GameEntityPool;
        if (p != null)
        {
            GenericCreationWindow.Show<ExtensionFactory>((f) =>
            {
                List<ExtensionFactory> l = p.ExtensionFactories == null ? new List<ExtensionFactory>() : p.ExtensionFactories.ToList();
                l.Add((ExtensionFactory)f);
                p.ExtensionFactories = l.ToArray();
            },
            p);
        }
    }
    private void Remove(UnityEngine.Object toRemove, UnityEngine.Object parent)
    {
        FactionProperties f = toRemove as FactionProperties;
        if (f != null)
        {
            Remove(f);
            RenderTree();
            return;
        }
        EntityGroup g = toRemove as EntityGroup;
        if (g != null)
        {
            Remove(g, parent);
            RenderTree();
            return;
        }
        GameEntityPool p = toRemove as GameEntityPool;
        if (p != null)
        {
            Remove(p, parent as EntityGroup);
            RenderTree();
            return;
        }
        ExtensionFactory x = toRemove as ExtensionFactory;
        if (x != null)
        {
            Remove(x, parent as GameEntityPool);
            RenderTree();
            return;
        }
    }
    private void Remove(FactionProperties faction)
    {
        foreach (EntityGroup group in new List<EntityGroup>(faction.entityGroups))
            Remove(group, faction);
        RemoveAsset(faction);
    }
    private void Remove(EntityGroup group, UnityEngine.Object parent)
    {
        FactionProperties f = parent as FactionProperties;
        if (f != null)
            f.entityGroups.Remove(group);
        else
        {
            EntityGroup g = parent as EntityGroup;
            g.children.Remove(group);
        }
        foreach (EntityGroup g in new List<EntityGroup>(group.children))
            Remove(g, group);
        foreach (GameEntityPool e in new List<GameEntityPool>(group.entities))
            Remove(e, group);
        RemoveAsset(group);
    }
    private void Remove(GameEntityPool gameEntityPool, EntityGroup parent)
    {
        parent.entities.Remove(gameEntityPool);
        if(gameEntityPool.ExtensionFactories != null)
            foreach (ExtensionFactory property in new List<ExtensionFactory>(gameEntityPool.ExtensionFactories))
                Remove(property, gameEntityPool);
        RemoveAsset(gameEntityPool);
    }
    private void Remove(ExtensionFactory property, GameEntityPool parent)
    {
        List<ExtensionFactory> l = new List<ExtensionFactory>(parent.ExtensionFactories);
        l.Remove(property);
        parent.ExtensionFactories = l.ToArray();
        RemoveAsset(property);
    }
    private void RemoveAsset(UnityEngine.Object o)
    {
        if (!treeView.duplicateMap.TryGetValue(o, out int value) || value <= 1)
        {
            AssetDatabase.RemoveObjectFromAsset(o);
            AssetDatabase.SaveAssets();
        }
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
        (pool) => treeView.CreateTreeViewItems(pool.ExtensionFactories)));
        this.treeView = treeView;
    }
}