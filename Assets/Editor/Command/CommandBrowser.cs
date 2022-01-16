using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using JHiga.RTSEngine.CommandPattern;

public class CommandBrowser : EditorWindow
{
    public CommandData Container
    {
        get
        {
            if (CommandData.Instance == null)
            {
                AssetDatabase.CreateAsset(CreateInstance<CommandData>(), "Assets/Resources/CommandData.asset");
                AssetDatabase.SaveAssets();
            }
            return CommandData.Instance;
        }
    }
    [MenuItem("RTS/Commands")]
    static void Init()
    {
        CommandBrowser window = GetWindow<CommandBrowser>();
        window.titleContent = new GUIContent("Command Browser");
        window.UpdateView();
        window.Show();
    } 
    public void UpdateView()
    {
        rootVisualElement.Clear();
        var items = new List<CommandProperties>(Container.commands);
        items.Add(null);
        Func<VisualElement> makeItem = () => new VisualElement();
        Action<VisualElement, int> bindItem = (e, i) =>
        {
            Button b = new Button();
            b.text = items[i] == null ? "Create" : "Edit";
            b.clicked += () => CommandEditor.Show(items[i], this);
            e.Add(b);

            if (items[i] != null)
            {
                Label l = new Label();
                l.text = items[i].name;
                e.Add(l);
                e.style.alignItems = Align.Center;
                e.style.borderTopWidth = .5f;
                e.style.borderTopColor = new StyleColor(new Color(0,0,0,0.4f));
                e.style.flexDirection = FlexDirection.Row;
            }
        };
        const int itemHeight = 50;
        var listView = new ListView(items, itemHeight, makeItem, bindItem);
        listView.selectionType = SelectionType.None;
        listView.style.flexGrow = 1.0f;
        rootVisualElement.Add(listView);
    }
    public void OnEnable()
    {
        UpdateView();
    }
}
