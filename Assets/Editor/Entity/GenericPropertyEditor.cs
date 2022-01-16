using JHiga.RTSEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GenericPropertyEditor : EditorWindow
{
    private Editor cacheEditor;
    private SerializedObject cache;
    public static void Show(Object o)
    {
        GenericPropertyEditor window = GetWindow<GenericPropertyEditor>();
        window.cache = new SerializedObject(o);
        window.cacheEditor = Editor.CreateEditor(o);
        window.Show();
    }
    private void OnGUI()
    {
        cacheEditor.DrawDefaultInspector();
    }
}
