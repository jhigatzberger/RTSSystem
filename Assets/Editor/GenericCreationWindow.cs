using JHiga.RTSEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class GenericCreationWindow : EditorWindow
{
    private Editor cacheEditor;
    private UnityEngine.Object cache;
    private int index = 0;
    private int subTypeIndex;
    private CreationType creationType = 0;
    private Action<UnityEngine.Object> addToContainerAction;
    private ICollection<UnityEngine.Object> originals;
    private UnityEngine.Object parent;
    private Type type;
    private Type[] subTypes;
    private string[] subTypeNames;
    public static void Show<T>(Action<UnityEngine.Object> addToContainerAction, UnityEngine.Object parent = null) where T : UnityEngine.Object
    {
        GenericCreationWindow window = GetWindow<GenericCreationWindow>();
        window.addToContainerAction = addToContainerAction;
        window.parent = parent;
        window.type = typeof(T);
        List<Type> types = new List<Type>(TypeCache.GetTypesDerivedFrom(window.type).Where(t => !t.IsAbstract).OrderBy(t => t.Name));
        if(!window.type.IsAbstract)
            types.Add(window.type);
        window.subTypes = types.ToArray();
        window.subTypeNames = window.subTypes.Select(s => s.Name).OrderBy(s=>s).ToArray();
        window.titleContent = new GUIContent(window.type.Name + " Editor");
        if (window.subTypes.Length>0)
            window.originals = Resources.LoadAll("", window.subTypes[0]);
        window.Show();
    }
    
    void OnGUI()
    {
        CreationTypeGUI();
        if(creationType == CreationType.New)
            CreateNewGUI();
        if (creationType == CreationType.Copy)
            CreateCopyGUI();
        if (creationType == CreationType.Link)
            CreateLinkGUI();

        if(creationType != CreationType.Link)
            cacheEditor.OnInspectorGUI();
        if (GUILayout.Button("Save"))
            Save();
        if (GUILayout.Button("Cancel"))
            Close();
    }
    public enum CreationType
    {
        New,
        Link,
        Copy
    }

    private void CreationTypeGUI()
    {
        List<string> creationTypes = new List<string>();
        creationTypes.Add(CreationType.New.ToString());
        if(originals != null && originals.Count > 0)
        {
            creationTypes.Add(CreationType.Link.ToString());
            creationTypes.Add(CreationType.Copy.ToString());
        }

        GUILayout.BeginHorizontal();
        CreationType _creationType = (CreationType)EditorGUILayout.Popup((int)creationType, creationTypes.ToArray());
        int _subTypeIndex = EditorGUILayout.Popup(subTypeIndex, subTypeNames);
        GUILayout.EndHorizontal();

        if (_creationType != creationType || _subTypeIndex != subTypeIndex)
        {
            index = 0;
            cache = null;
        }
        if (_subTypeIndex != subTypeIndex)
            originals = Resources.LoadAll("", subTypes[_subTypeIndex]);
        subTypeIndex = _subTypeIndex;
        creationType = _creationType;
    }
    private void CreateLinkGUI()
    {
        EditorGUILayout.LabelField("Original");
        List<string> templateList = new List<string>(originals.Select(e => e.name));
        string[] templateNames = templateList.ToArray();
        int _index = EditorGUILayout.Popup(index, templateNames);
        if (index != _index || cache == null)
        {
            index = _index;
            cache = originals.First(e => e.name.Equals(templateNames[index]));
        }
    }

    private void CreateCopyGUI()
    {        
        EditorGUILayout.LabelField("Original");
        List<string> templateList = new List<string>(originals.Select(e => e.name));
        string[] templateNames = templateList.ToArray();
        int _index = EditorGUILayout.Popup(index, templateNames);
        if (index != _index || cache == null || cacheEditor == null)
        {
            index = _index;
            cache = Instantiate(originals.First(e => e.name.Equals(templateNames[index])));
            cacheEditor = Editor.CreateEditor(cache);
            cache.name = templateNames[index];            
        }
    }
    private void CreateNewGUI()
    {
        if (cache == null)
        {
            cache = CreateInstance(subTypes[subTypeIndex]);
            cacheEditor = Editor.CreateEditor(cache);
            cache.name = subTypeNames[subTypeIndex];
        }
    }
    void Save()
    {
        if (parent != null && creationType != CreationType.Link)
        {
            AssetDatabase.AddObjectToAsset(cache, parent);
            AssetDatabase.SaveAssets();
            EditorUtility.SetDirty(cache);
            EditorUtility.SetDirty(parent);
        }
        if (addToContainerAction != null)
            addToContainerAction(cache);
        EntityTreeViewWindow.Instance.RenderTree();
        Close();
    }
}