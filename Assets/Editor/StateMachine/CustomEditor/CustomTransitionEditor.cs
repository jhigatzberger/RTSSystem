using UnityEngine;
using UnityEditor;
using JHiga.RTSEngine.StateMachine;

[CustomEditor(typeof(Transition))]
[CanEditMultipleObjects]
public class CustomTransitionEditor : Editor
{
    SerializedProperty decision;
    SerializedProperty trueState;
    SerializedProperty falseState;


    void OnEnable()
    {
        decision = serializedObject.FindProperty("decision");
        trueState = serializedObject.FindProperty("trueState");
        falseState = serializedObject.FindProperty("falseState");
    }

    public override void OnInspectorGUI()
    {
        if (decision == null || falseState == null || trueState == null)
            return;
        serializedObject.Update();
        serializedObject.targetObject.name = EditorGUILayout.TextField("Name: ", serializedObject.targetObject.name);
        GUILayout.BeginHorizontal();
        GUILayout.Label("Decision: ");
        GUILayout.Label(decision.objectReferenceValue == null ? "null" : decision.objectReferenceValue.name);
        if(GUILayout.Button("Edit Decision"))
            TypePickerWindow.Show<Decision>((o) => decision.objectReferenceValue = o, target);
        GUILayout.EndHorizontal();
        EditorGUILayout.PropertyField(trueState);
        EditorGUILayout.PropertyField(falseState);
        serializedObject.ApplyModifiedProperties();
    }

    const int WIDTH_OFFSET = 10;
    const int HEIGHT_OFFSET = 120;

    private static int ImageSize
    {
        get
        {
            if (Screen.width / 2 - WIDTH_OFFSET < Screen.height - HEIGHT_OFFSET)
                return Screen.width / 2 - WIDTH_OFFSET;
            else
                return Screen.height - HEIGHT_OFFSET;

        }
    }
}