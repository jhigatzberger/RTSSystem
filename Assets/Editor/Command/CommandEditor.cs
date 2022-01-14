using JHiga.RTSEngine.CommandPattern;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class CommandEditor : EditorWindow
{
    private Editor cacheEditor;
    private CommandProperties cache;
    private int index = 0;

    private bool IsNew => EditorCommandView.Cache == null;


    public void OnEnable()
    {
        cache = EditorCommandView.Cache;
        if (cache != null)
            cacheEditor = Editor.CreateEditor(cache);
    }

    void OnGUI()
    {
        if (IsNew)
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
        if (GUILayout.Button(IsNew ? "Create":"Save"))
            Save();
        if (GUILayout.Button(IsNew ? "Cancel":"Remove"))
            Remove();
    }

    void Save()
    {
        if (!EditorCommandView.Container.commands.Contains(cache))
        {
            AssetDatabase.AddObjectToAsset(cache, EditorCommandView.Container);
            EditorCommandView.Container.commands.Add(cache);
        }

        AssetDatabase.SaveAssets();

        EditorUtility.SetDirty(cache);
        EditorUtility.SetDirty(EditorCommandView.Container);

        if (EditorCommandView.Instance != null)
        {
            EditorCommandView.Instance.Repaint();
            EditorCommandView.Instance.UpdateView();
        }
        Close();
    }
    void Remove()
    {
        if (EditorCommandView.Container.commands.Contains(cache))
        {
            AssetDatabase.RemoveObjectFromAsset(cache);
            EditorCommandView.Container.commands.Remove(cache);
        }

        AssetDatabase.SaveAssets();

        EditorUtility.SetDirty(cache);
        EditorUtility.SetDirty(EditorCommandView.Container);

        if (EditorCommandView.Instance != null)
        {
            EditorCommandView.Instance.Repaint();
            EditorCommandView.Instance.UpdateView();
        }
        Close();
    }
}
