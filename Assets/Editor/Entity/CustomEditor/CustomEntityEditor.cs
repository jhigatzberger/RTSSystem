using UnityEngine;
using UnityEditor;
using JHiga.RTSEngine;
using System.Linq;
using System.Collections.Generic;

[CustomEditor(typeof(GameEntityPool))]
[CanEditMultipleObjects]
public class CustomEntityEditor : Editor
{
    SerializedProperty prefab;
    Texture2D _texture;
    Texture2D Texture {
        get
        {
            if ((_texture == null || serializedObject.hasModifiedProperties) && prefab.objectReferenceValue != null)
                _texture = GetPrefabPreview(PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(prefab.objectReferenceValue));

            return _texture;
        }
    }
    void OnEnable()
    {
        prefab = serializedObject.FindProperty("prefab");      
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        serializedObject.targetObject.name = EditorGUILayout.TextField("Name: ", serializedObject.targetObject.name);
        if(Texture != null)
            GUILayout.Box(Texture);
        EditorGUILayout.PropertyField(prefab, GUIContent.none, Texture);
        serializedObject.ApplyModifiedProperties();
    }

    const int WIDTH_OFFSET = 10;
    const int HEIGHT_OFFSET = 120;

    private static int ImageSize
    {
        get
        {
            if (Screen.width / 2- WIDTH_OFFSET < Screen.height- HEIGHT_OFFSET)
                return Screen.width/2- WIDTH_OFFSET;
            else
                return Screen.height - HEIGHT_OFFSET;

        }
    }

    static Texture2D GetPrefabPreview(string path)
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        var editor = CreateEditor(prefab);
        Texture2D tex = editor.RenderStaticPreview(path, null, ImageSize, ImageSize);
        DestroyImmediate(editor);
        return tex;
    }
}