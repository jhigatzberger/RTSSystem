using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.IMGUI.Controls;

class GenericTreeView : TreeView
{
    public GenericTreeView(TreeViewState treeViewState)
        : base(treeViewState) { }
    public UnityEngine.Object selectedObject;
    private Dictionary<int, UnityEngine.Object> unityObjects = new Dictionary<int, UnityEngine.Object>();
    private int _lastItemId = -1;
    private int NextItemId { get => ++_lastItemId; }

    protected override TreeViewItem BuildRoot()
    {
        var root = new TreeViewItem { id = NextItemId, depth = -1, displayName = "_Root" };

        foreach (TreeViewItem item in RootChildren)
            root.AddChild(item);

        SetupDepthsFromParentsAndChildren(root);
        return root;
    }
    protected override void SingleClickedItem(int id)
    {
        selectedObject = unityObjects[id];
    }
    public UnityEngine.Object GetParent()
    {
        if (GetSelection().Count == 0)
            return null;
        TreeViewItem item = FindItem(GetSelection().First(), rootItem);
        return unityObjects[item.parent.id];
    }
    private TreeViewItem GetTreeViewItem<T>(T o, Func<T, TreeViewItem[]> children) where T : UnityEngine.Object
    {
        int id = NextItemId;
        TreeViewItem item = new TreeViewItem
        {
            id = id,
            displayName = o.name
        };
        unityObjects.Add(id, o);
        if (children != null)
        {
            foreach (TreeViewItem child in children(o))
                item.AddChild(child);
        }
        return item;
    }
    public TreeViewItem[] GetTreeViewItems<T>(ICollection<T> source, Func<T, TreeViewItem[]> childrenFunc = null) where T : UnityEngine.Object
    {
        List<TreeViewItem> items = new List<TreeViewItem>();
        foreach (T t in source)
            items.Add(GetTreeViewItem(t, childrenFunc));
        return items.ToArray();
    }

    private TreeViewItem[] _rootChildren;
    public TreeViewItem[] RootChildren
    {
        set
        {
            _rootChildren = value;
            Reload();
        }
        get => _rootChildren;
    }
}
