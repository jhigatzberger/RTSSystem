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
    public Dictionary<UnityEngine.Object, int> duplicateMap = new Dictionary<UnityEngine.Object, int>();
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
        return unityObjects.TryGetValue(item.parent.id, out UnityEngine.Object value) ? value : null;
    }
    private TreeViewItem CreateItem(UnityEngine.Object o)
    {
        int id = NextItemId;
        TreeViewItem item = new TreeViewItem
        {
            id = id,
            displayName = o.name
        };
        unityObjects.Add(id, o);
        if (!duplicateMap.TryGetValue(o, out int count))
            count = 0;
        duplicateMap[o] = ++count;
        return item;
    }
    private TreeViewItem CreateTreeViewItem<T>(T o, Func<T, TreeViewItem[]> children) where T : UnityEngine.Object
    {
        if (o == null)
            return null;
        TreeViewItem item = CreateItem(o);
        if (children != null)
        {
            foreach (TreeViewItem child in children(o))
                item.AddChild(child);
        }
        return item;
    }
    public TreeViewItem[] CreateTreeViewItems<T>(ICollection<T> source, Func<T, TreeViewItem[]> childrenFunc = null) where T : UnityEngine.Object
    {
        if (source == null)
            return new TreeViewItem[] { };
        List<TreeViewItem> items = new List<TreeViewItem>();
        foreach (T t in source)
            items.Add(CreateTreeViewItem(t, childrenFunc));
        return items.ToArray();
    }
    public TreeViewItem[] CreateRecursiveReflectiveTreeViewItems<T, NextElementType>(ICollection<T> source, string childCollectionName, string nextElementCollectionName, Func<NextElementType, TreeViewItem[]> childrenFunc = null)
        where T : UnityEngine.Object
        where NextElementType : UnityEngine.Object
    {
        if (source == null)
            return new TreeViewItem[] { };
        List<TreeViewItem> items = new List<TreeViewItem>();
        foreach (T t in source)
            items.Add(GetRecursiveReflectiveTreeViewItem(t, childCollectionName, nextElementCollectionName, childrenFunc));
        return items.ToArray();
    }
    private TreeViewItem GetRecursiveReflectiveTreeViewItem<T, NextElementType>(T o, string childCollectionName, string nextElementCollectionName, Func<NextElementType, TreeViewItem[]> childrenFunc = null)
        where T : UnityEngine.Object
        where  NextElementType : UnityEngine.Object
    {
        TreeViewItem parent = CreateItem(o);
        foreach ( NextElementType nextElement in (ICollection<NextElementType>)o.GetType().GetField(nextElementCollectionName).GetValue(o))
            parent.AddChild(CreateTreeViewItem(nextElement, childrenFunc));
        foreach (T c in (ICollection<T>)o.GetType().GetField(childCollectionName).GetValue(o))
            parent.AddChild(GetRecursiveReflectiveTreeViewItem(c, childCollectionName, nextElementCollectionName, childrenFunc));
        return parent;
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
