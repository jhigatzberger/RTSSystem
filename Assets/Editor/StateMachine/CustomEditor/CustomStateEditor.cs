using UnityEngine;
using UnityEditor;
using JHiga.RTSEngine;
using System.Linq;
using System.Collections.Generic;
using JHiga.RTSEngine.StateMachine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

[CustomEditor(typeof(State))]
[CanEditMultipleObjects]
public class CustomStateEditor : Editor
{
    public override VisualElement CreateInspectorGUI()
    {
        return new NameField(target);
    }
}