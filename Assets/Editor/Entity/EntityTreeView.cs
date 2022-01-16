using JHiga.RTSEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

class EntityTreeView : TreeView
{
    public EntityTreeView(TreeViewState treeViewState)
        : base(treeViewState)
    {
        Reload();
    }
    public enum EntityTreeDepth
    {
        _Root = -1,
        Root = 0,
        Faction = 1,
        Group = 2,
        Entity = 3,
        Component = 4
    }
    private int _lastItemId = -1;
    private int NextItemId { get => ++_lastItemId; }
    protected override TreeViewItem BuildRoot()
    {
        var root = new TreeViewItem { id = NextItemId, depth = (int)EntityTreeDepth._Root, displayName = "_Root" };
        var factions = new TreeViewItem { id = NextItemId, displayName = "Factions" };

        root.AddChild(factions);
        foreach (TreeViewItem item in GetFactions())
            factions.AddChild(item);       

        SetupDepthsFromParentsAndChildren(root);
        return root;
    }

    protected override void SingleClickedItem(int id)
    {
        TreeViewItem item = FindItem(id, rootItem);
        Debug.Log((EntityTreeDepth)item.depth);
        switch ((EntityTreeDepth) item.depth)
        {
            case EntityTreeDepth.Faction:
                {
                    if (factions.TryGetValue(id, out FactionProperties value))
                        GenericPropertyEditor.Show(value);
                    else
                        throw new NotImplementedException("implement creating new factions");
                    break;
                }
            case EntityTreeDepth.Group:
                {
                    throw new NotImplementedException("implement group editor");
                }
            case EntityTreeDepth.Entity:
                {
                    if (entites.TryGetValue(id, out GameEntityFactory value))
                        GenericPropertyEditor.Show(value);
                    else
                        throw new NotImplementedException("implement creating new entites");
                    break;
                }
            case EntityTreeDepth.Component:
                {
                    if (entityProperties.TryGetValue(id, out ExtensionFactory value))
                        GenericPropertyEditor.Show(value);
                    else
                        throw new NotImplementedException("implement adding new components");
                    break;
                }
        }
    }
    private TreeViewItem AddNode
    {
        get => new TreeViewItem
        {
            id = NextItemId,
            displayName = "+"
        };
    }

    private Dictionary<int, FactionProperties> factions = new Dictionary<int, FactionProperties>();
    private TreeViewItem[] GetFactions()
    {
        List<TreeViewItem> items = new List<TreeViewItem>();
        foreach(FactionProperties f in Resources.LoadAll<FactionProperties>(""))
        {
            int id = NextItemId;
            TreeViewItem item = new TreeViewItem
            {
                id = id,
                displayName = f.name
            };
            factions.Add(id, f);
            foreach (TreeViewItem child in GetEntityGroups(f))
                item.AddChild(child);
            items.Add(item);
        }
        items.Add(AddNode);
        return items.ToArray();
    }

    private Dictionary<int, EntityGroup> groups = new Dictionary<int, EntityGroup>();
    private TreeViewItem[] GetEntityGroups(FactionProperties faction)
    {
        List<TreeViewItem> items = new List<TreeViewItem>();
        foreach (EntityGroup g in faction.entityGroups)
        {
            int id = NextItemId;
            TreeViewItem item = new TreeViewItem
            {
                id = id,
                displayName = g.name
            };
            groups.Add(id, g);
            foreach (TreeViewItem child in GetEntities(g))
                item.AddChild(child);
            items.Add(item);
        }
        items.Add(AddNode);
        return items.ToArray();
    }

    private Dictionary<int, GameEntityFactory> entites = new Dictionary<int, GameEntityFactory>();
    private TreeViewItem[] GetEntities(EntityGroup group)
    {
        List<TreeViewItem> items = new List<TreeViewItem>();
        foreach (GameEntityFactory e in group.entities)
        {
            int id = NextItemId;
            TreeViewItem item = new TreeViewItem
            {
                id = id,
                displayName = e.name
            };
            entites.Add(id, e);
            foreach (TreeViewItem child in GetEntityProperties(e))
                item.AddChild(child);
            items.Add(item);
        }
        items.Add(AddNode);
        return items.ToArray();
    }

    private Dictionary<int, ExtensionFactory> entityProperties = new Dictionary<int, ExtensionFactory>();
    private TreeViewItem[] GetEntityProperties(GameEntityFactory entity)
    {
        List<TreeViewItem> items = new List<TreeViewItem>();
        foreach (ExtensionFactory ef in entity.ExtensionFactories)
        {
            int id = NextItemId;
            TreeViewItem item = new TreeViewItem
            {
                id = id,
                displayName = ef.name
            };
            entityProperties.Add(id, ef);
            items.Add(item);
        }
        items.Add(AddNode);
        return items.ToArray();
    }
}