using UnityEngine;
using UnityEditor;
using JHiga.RTSEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

[CustomEditor(typeof(GameEntityPool))]
[CanEditMultipleObjects]
public class CustomEntityEditor : Editor
{
    SerializedProperty prefabs;
    Texture2D _texture;
    Texture2D Texture {
        get
        {
            if ((_texture == null || serializedObject.hasModifiedProperties) && prefabs.arraySize > 0 && prefabs.GetArrayElementAtIndex(0).objectReferenceValue != null)
                _texture = GetPrefabPreview(PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(prefabs.GetArrayElementAtIndex(0).objectReferenceValue));

            return _texture;
        }
    }
    void OnEnable()
    {
        try
        {
            prefabs = serializedObject.FindProperty("prefabs");
        }
        catch
        {
            prefabs = null;
        }
    }

    public override void OnInspectorGUI()
    {
        if (prefabs == null)
            return;
        serializedObject.Update();
        serializedObject.targetObject.name = EditorGUILayout.TextField("Name: ", serializedObject.targetObject.name);
        if(Texture != null)
            GUILayout.Box(Texture);
        EditorGUILayout.PropertyField(prefabs, GUIContent.none, Texture);
        serializedObject.ApplyModifiedProperties();
    }
    private Image preview;
    public override VisualElement CreateInspectorGUI()
    {
        VisualElement inspector = new VisualElement();
        inspector.Add(new NameField(target));
        preview = new Image();
        preview.image = Texture;
        PropertyField prefabField = new PropertyField();
        prefabField.Bind(serializedObject);
        prefabField.BindProperty(prefabs);
        prefabField.RegisterCallback<ChangeEvent<Object>>(e =>
        {
            preview.image = Texture;
        });
        inspector.Add(preview);
        inspector.Add(prefabField);
        //inspector.Add(new DefaultInspectorElement(serializedObject));
        return inspector;
    }

    static Texture2D GetPrefabPreview(string path)
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        var editor = CreateEditor(prefab);
        Texture2D tex = editor.RenderStaticPreview(path, null, 1024, 1024);
        DestroyImmediate(editor);
        return tex;
    }
}