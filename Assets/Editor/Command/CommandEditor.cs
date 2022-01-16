using JHiga.RTSEngine.CommandPattern;
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class CommandEditor : EditorWindow
{
    private Editor cacheEditor;
    private CommandProperties cache;
    private CommandBrowser browser;
    private bool isCreate;
    private int index = 0;
    public static void Show(CommandProperties properties, CommandBrowser browser)
    {
        CommandEditor window = GetWindow<CommandEditor>();
        window.titleContent = new GUIContent("Command Editor");
        window.cache = properties;
        window.browser = browser;
        window.isCreate = properties == null;
        if (properties != null)
            window.cacheEditor = Editor.CreateEditor(properties);
        window.Show();
    }
    void OnGUI()
    {
        if (isCreate)
        {
            string[] types = TypeCache.GetTypesDerivedFrom<CommandProperties>().Where(t => !t.IsAbstract).Select(t => t.Name).ToArray();
            int _index = EditorGUILayout.Popup(index, types);
            if (index != _index || cache == null || cacheEditor == null)
            {
                index = _index;
                cache = CreateInstance(types[index]) as CommandProperties;
                cacheEditor = Editor.CreateEditor(cache);
                cache.name = types[index];
            }           
        }
        cache.name = EditorGUILayout.TextField("Command Name: ", cache.name);
        cacheEditor.DrawDefaultInspector();
        if (GUILayout.Button(isCreate ? "Create":"Save"))
            Save();
        if (GUILayout.Button(isCreate ? "Cancel":"Remove"))
            Remove();
    }
    void Save()
    {
        if (!browser.Container.commands.Contains(cache))
        {
            AssetDatabase.AddObjectToAsset(cache, browser.Container);
            browser.Container.commands.Add(cache);
        }
        AssetDatabase.SaveAssets();
        EditorUtility.SetDirty(cache);
        EditorUtility.SetDirty(browser.Container);
        browser.Repaint();
        browser.UpdateView();
        Close();
    }
    void Remove()
    {
        if (browser.Container.commands.Contains(cache))
        {
            AssetDatabase.RemoveObjectFromAsset(cache);
            browser.Container.commands.Remove(cache);
        }
        AssetDatabase.SaveAssets();
        EditorUtility.SetDirty(cache);
        EditorUtility.SetDirty(browser.Container);
        browser.Repaint();
        browser.UpdateView();
        Close();
    }
}
