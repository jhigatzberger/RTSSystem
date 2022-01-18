using UnityEngine;
using UnityEditor.IMGUI.Controls;
using UnityEditor;
using JHiga.RTSEngine;
using JHiga.RTSEngine.CommandPattern;
using System;
using System.Collections.Generic;
using System.Linq;

class CommandTreeViewWindow : EditorWindow
{
    // SerializeField is used to ensure the view state is written to the window 
    // layout file. This means that the state survives restarting Unity as long as the window
    // is not closed. If the attribute is omitted then the state is still serialized/deserialized.
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

    //The TreeView is not serializable, so it should be reconstructed from the tree data.
    GenericTreeView m_SimpleTreeView;

    private Editor cacheEditor;
    private UnityEngine.Object cache;
    void OnGUI()
    {
        m_SimpleTreeView.OnGUI(new Rect(0, 0, position.width/2, position.height));
        
        if(m_SimpleTreeView.selectedObject != null && cache != m_SimpleTreeView.selectedObject)
        {
            cache = m_SimpleTreeView.selectedObject;
            cacheEditor = Editor.CreateEditor(cache);
        }
        GUILayout.BeginHorizontal();
        GUILayout.Space(position.width / 2);
        GUILayout.BeginVertical();
        if (cacheEditor != null)
            cacheEditor.OnInspectorGUI(); //.DrawDefaultInspector();
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
    }

    [MenuItem("RTS/Command Tree")]
    static void ShowWindow()
    {
        var window = GetWindow<CommandTreeViewWindow>();
        window.titleContent = new GUIContent("Command Tree");
        GenericTreeView tv = new GenericTreeView(EntityTreeViewState);
        tv.RootChildren = tv.GetTreeViewItems(CommandType.LoadAll(),
            (commandType) => tv.GetTreeViewItems(commandType.commandProperties)
        );
        window.m_SimpleTreeView = tv;
        window.Show();        
    }
}
public class CommandType : ScriptableObject
{
    public CommandProperties[] commandProperties;
    private static CommandType Load(Type t)
    {
        CommandType commandType = CreateInstance<CommandType>();
        commandType.commandProperties = Resources.LoadAll("", t).Select(o => (CommandProperties)o).ToArray();
        commandType.name = t.Name;
        return commandType;
    }
    public static List<CommandType> LoadAll()
    {
        List<CommandType> types = new List<CommandType>();
        foreach (Type t in TypeCache.GetTypesDerivedFrom<CommandProperties>())
        {
            Debug.Log("loading " + t.Name);
            if(!t.IsAbstract)
                types.Add(Load(t));
        }
        return types;
    }
}