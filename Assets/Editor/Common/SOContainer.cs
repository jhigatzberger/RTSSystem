using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SOContainer : ScriptableObject
{
    public string typeName;

    public void Load()
    {
        if (!_containers.ContainsKey(typeName))
            _containers.Add(typeName, this);
    }
    private static bool loaded;
    private static Dictionary<string, SOContainer> _containers = new Dictionary<string, SOContainer>();

    public static SOContainer Get<T>()
    {
        if (!loaded)
            LoadAll();
        if (!_containers.TryGetValue(typeof(T).Name, out SOContainer container))
        {
            container = CreateInstance<SOContainer>();
            container.typeName = typeof(T).Name;
            container.Load();
            EditorUtility.SetDirty(container);
            AssetDatabase.CreateAsset(container, Path.Generate<T>(suffix: "Container"));
            AssetDatabase.SaveAssets();
        }
        return container;
    }

    private static void LoadAll()
    {
        foreach (SOContainer c in Resources.LoadAll<SOContainer>(""))
            c.Load();
        loaded = true;
    }
}
