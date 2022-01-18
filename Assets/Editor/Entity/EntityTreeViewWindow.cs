using UnityEngine;
using UnityEditor.IMGUI.Controls;
using UnityEditor;
using JHiga.RTSEngine;
using System.Collections.Generic;
using System.Linq;

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
    private Object cache;
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
            if (!(treeView.selectedObject is ExtensionFactory) && GUILayout.Button("Add to " + cache.name))
            {
                Object selectedObject = treeView.selectedObject;
                if (selectedObject is FactionProperties)
                {
                    FactionProperties faction = (FactionProperties)selectedObject;
                    GenericCreationWindow.Show<EntityGroup>((g) => faction.entityGroups.Add((EntityGroup)g), faction);
                }
                else if (selectedObject is EntityGroup)
                {
                    EntityGroup group = (EntityGroup)selectedObject;
                    GenericCreationWindow.Show<GameEntityFactory>((e) => group.entities.Add((GameEntityFactory)e), group);
                }
                else if (selectedObject is GameEntityFactory)
                {
                    GameEntityFactory entity = (GameEntityFactory)selectedObject;
                    GenericCreationWindow.Show<ExtensionFactory>((f) =>
                    {
                        List<ExtensionFactory> l = entity.ExtensionFactories == null ? new List<ExtensionFactory>() : entity.ExtensionFactories.ToList();
                        l.Add((ExtensionFactory)f);
                        entity.ExtensionFactories = l.ToArray();
                    },
                    entity);
                }
            }
            if (GUILayout.Button("Remove " + cache.name))
            {
                Object selectedObject = treeView.selectedObject;
                if (selectedObject == null)
                    throw new System.Exception("selectedObject is null!");
                if (!(selectedObject is FactionProperties))
                {
                    Object parent = treeView.GetParent();
                    if (parent is FactionProperties)
                        ((FactionProperties)parent).entityGroups.Remove((EntityGroup)selectedObject);
                    else if (parent is EntityGroup)
                        ((EntityGroup)parent).entities.Remove((GameEntityFactory)selectedObject);
                    else if (parent is GameEntityFactory)
                    {
                        GameEntityFactory entity = ((GameEntityFactory)parent);
                        List<ExtensionFactory> l = new List<ExtensionFactory>(entity.ExtensionFactories);
                        l.Remove((ExtensionFactory)selectedObject);
                        entity.ExtensionFactories = l.ToArray();
                    }
                }
                AssetDatabase.RemoveObjectFromAsset(selectedObject);
                AssetDatabase.SaveAssets();
                RenderTree();
            }
        } 
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
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
        treeView.RootChildren = treeView.GetTreeViewItems(Resources.LoadAll<FactionProperties>(""),
        (faction) => treeView.GetTreeViewItems(faction.entityGroups,
        (group) => treeView.GetTreeViewItems(group.entities,
        (entity) => treeView.GetTreeViewItems(entity.ExtensionFactories))));
        this.treeView = treeView;
    }
}