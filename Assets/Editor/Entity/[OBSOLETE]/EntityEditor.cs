using JHiga.RTSEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class EntityEditor : EditorWindow
{
    private EntityBrowser browser;
    private Editor cacheEditor;
    private GameEntityFactory cache;
    private bool isCreate;
    private int typeIndex = 0;
    private int templateIndex = 0;
    private List<GameEntityFactory> gameEntityFactories;
    public static void Show(GameEntityFactory entityFactory, EntityBrowser browser)
    {
        EntityEditor window = GetWindow<EntityEditor>();
        window.cache = entityFactory;
        window.browser = browser;
        window.isCreate = entityFactory == null;
        if(entityFactory != null)
            window.cacheEditor = Editor.CreateEditor(entityFactory);
        window.gameEntityFactories = browser.Container.GetEntitiesFromAllGroups();
        window.Show();
    }
    /*
    void OnGUI()
    {
        if (isCreate)
        {
            if(templateIndex == 0)
                CreateTypeGUI();
            CreateTemplateGUI();
        }
        cache.name = EditorGUILayout.TextField("Entity Name: ", cache.name);
        cacheEditor.DrawDefaultInspector();
        if (GUILayout.Button(isCreate ? "Create" : "Save"))
            Save();
        if (GUILayout.Button(isCreate ? "Cancel" : "Remove"))
            Remove();
    }

    private void CreateTemplateGUI()
    {
        
        EditorGUILayout.LabelField("Duplicate Original");
        List<string> templateList = new List<string>(browser.Container.entities.Select(e => e.name));
        templateList.Insert(0, "None");
        string[] templates = templateList.ToArray();
        int _templateIndex = EditorGUILayout.Popup(templateIndex, templates);
        if (templateIndex != _templateIndex || cache == null || cacheEditor == null)
        {
            templateIndex = _templateIndex;
            if (templateIndex > 0)
            {
                cache = Instantiate(browser.Container.entities.Find(e => e.name.Equals(templates[templateIndex])));
                cacheEditor = Editor.CreateEditor(cache);
                cache.name = templates[templateIndex];
            }
        }
    }
    private void CreateTypeGUI()
    {
        EditorGUILayout.LabelField("Type of Factory");
        string[] types = TypeCache.GetTypesDerivedFrom<GameEntityFactory>().Where(t => !t.IsAbstract).Select(t => t.Name).ToArray();
        int _typeIndex = EditorGUILayout.Popup(typeIndex, types);
        if (typeIndex != _typeIndex || cache == null || cacheEditor == null)
        {
            typeIndex = _typeIndex;
            cache = CreateInstance(types[typeIndex]) as GameEntityFactory;
            cacheEditor = Editor.CreateEditor(cache);
            cache.name = types[typeIndex];
        }
    }
    void Save()
    {
        if (!browser.Container.entities.Contains(cache))
        {
            AssetDatabase.AddObjectToAsset(cache, browser.Container);
            browser.Container.entities.Add(cache);
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
        if (browser.Container.entities.Contains(cache))
        {
            AssetDatabase.RemoveObjectFromAsset(cache);
            browser.Container.entities.Remove(cache);
        }
        AssetDatabase.SaveAssets();
        EditorUtility.SetDirty(cache);
        EditorUtility.SetDirty(browser.Container);
        browser.Repaint();
        browser.UpdateView();
        Close();
    }*/
}
