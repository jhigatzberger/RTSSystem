
using UnityEditor;
using JHiga.RTSEngine;
using UnityEngine.UIElements;
using System;
using System.Collections.Generic;
using UnityEngine;

public class FactionBrowser : EditorWindow
{
    static void Init()
    {
        FactionBrowser window = GetWindow<FactionBrowser>();
        window.UpdateView();
        window.Show();
    }
    public void UpdateView()
    {
        rootVisualElement.Clear();
        var items = new List<FactionProperties>(Resources.LoadAll<FactionProperties>(""));
        items.Add(null);
        Func<VisualElement> makeItem = () => new VisualElement();
        Action<VisualElement, int> bindItem = (e, i) =>
        {
            Button b = new Button();
            b.text = items[i] == null ? "Create" : "Edit";
            b.clicked += () =>
            {
                if (items[i] == null)
                    throw new NotImplementedException("implement faction creation");
                EntityBrowser.Show(items[i]);
            };
            e.Add(b);
            if (items[i] != null)
            {
                Label l = new Label();
                l.text = items[i].name;
                e.Add(l);
                e.style.alignItems = Align.Center;
                e.style.borderTopWidth = .5f;
                e.style.borderTopColor = new StyleColor(new Color(0, 0, 0, 0.4f));
                e.style.flexDirection = FlexDirection.Row;
            }
        };
        const int itemHeight = 50;
        var listView = new ListView(items, itemHeight, makeItem, bindItem);
        listView.selectionType = SelectionType.None;
        listView.style.flexGrow = 1.0f;
        rootVisualElement.Add(listView);
    }
}
