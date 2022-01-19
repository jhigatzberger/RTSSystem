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
                AssetDatabase.CreateAsset(CreateInstance<CommandData>(), "Assets/Resources/RTS/CommandData.asset");
                AssetDatabase.SaveAssets();
            }
            return CommandData.Instance;
        }
    }
    //[MenuItem("RTS/Commands")]
    static void Init()
    {
        CommandBrowser window = GetWindow<CommandBrowser>();
        window.titleContent = new GUIContent("Command Browser");
        window.UpdateView();
        window.Show();
    }
    const int itemHeight = 50;
    public void UpdateView()
    {
        rootVisualElement.Clear();
        var items = new List<CommandProperties>(Container.commands);
        Func<VisualElement> makeItem = () => new VisualElement();
        Action<VisualElement, int> bindItem = (e, i) =>
        {   
            if (items[i].icon != null)
            {
                Image image = new Image();
                image.image = items[i].icon.texture;
                image.style.width = itemHeight;
                image.style.height = itemHeight;
                e.Add(image);
            }
            Label l = new Label();
            l.text = items[i].name;
            e.Add(l);
            e.style.alignItems = Align.Center;
            e.style.borderTopWidth = .5f;
            e.style.borderTopColor = new StyleColor(new Color(0,0,0,0.4f));
            e.style.flexDirection = FlexDirection.Row;
        };
        var listView = new ListView(items, itemHeight, makeItem, bindItem);
        listView.selectionType = SelectionType.Single;
        listView.style.flexGrow = 1.0f;
        listView.onItemsChosen += obj => CommandEditor.Show((CommandProperties)new List<object>(obj)[0], this);
        
        rootVisualElement.Add(listView);

        Button b = new Button();
        b.text = "Create";
        b.clicked += () => CommandEditor.Show(null, this);
        rootVisualElement.Add(b);
    }
    public void OnEnable()
    {
        UpdateView();
    }
}
/*
protected override void SetupDragAndDrop(SetupDragAndDropArgs args)
{
    if (hasSearch)
        return;

    DragAndDrop.PrepareStartDrag();
    var draggedRows = GetRows().Where(item => args.draggedItemIDs.Contains(item.id)).ToList();
    DragAndDrop.SetGenericData(k_GenericDragID, draggedRows);
    DragAndDrop.objectReferences = new UnityEngine.Object[] { }; // this IS required for dragging to work
    string title = draggedRows.Count == 1 ? draggedRows[0].displayName : "< Multiple >";
    DragAndDrop.StartDrag(title);
}*/