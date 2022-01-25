using UnityEngine;
using UnityEditor.IMGUI.Controls;
using UnityEditor;
using JHiga.RTSEngine.CommandPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;

class CommandTreeViewWindow : EditorWindow
{
    private Button addButton;
    private Button removeButton;
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
    GenericTreeView treeView;

    private Editor CacheEditor
    {
        set
        {
            InspectorContainer.Clear();
            if (value != null)
            {
                removeButton.SetEnabled(Cache is CommandProperties);
                addButton.SetEnabled(Cache is CommandType);
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
            if (value != _cache)
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

        addButton = new Button(() => Add());
        addButton.text = "Add Child";
        rightHalfContainer.Add(addButton);

        removeButton = new Button(() => Remove((CommandProperties)Cache));
        removeButton.text = "Remove";
        rightHalfContainer.Add(removeButton);
    }
    void OnGUI()
    {
        treeView.OnGUI(new Rect(0, 0, position.width / 2, position.height));
        Cache = treeView.selectedObject;
    }
    private void Add()
    {
        TypePickerWindow.Show<CommandProperties>((c) =>
        {
            Container.commands.Add(c);
            EditorUtility.SetDirty(c);
            AssetDatabase.SaveAssets();
            RenderTree();
        }, Container, new TypePickerWindow.EditType[] { TypePickerWindow.EditType.New, TypePickerWindow.EditType.Copy });
    }
    private void Remove(CommandProperties properties)
    {
        Container.commands.Remove(properties);
        AssetDatabase.RemoveObjectFromAsset(properties);
        AssetDatabase.SaveAssets();
        EditorUtility.SetDirty(properties);
    }
    private CommandData Container
    {
        get
        {
            if (CommandData.Instance == null)
            {
                AssetDatabase.CreateAsset(CreateInstance<CommandData>(), Path.GenerateFull<CommandData>());
                AssetDatabase.SaveAssets();
            }
            return CommandData.Instance;
        }
    }

    [MenuItem("RTS/Command Tree")]
    static void ShowWindow()
    {
        var window = GetWindow<CommandTreeViewWindow>();
        window.titleContent = new GUIContent("Command Tree");
        window.RenderTree();
        window.Show();        
    }

    private void RenderTree()
    {
        GenericTreeView tv = new GenericTreeView(EntityTreeViewState);
        tv.RootChildren = tv.CreateTreeViewItems(CommandType.LoadAll(),
            (commandType) => tv.CreateTreeViewItems(commandType.commandProperties)
        );
        treeView = tv;
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