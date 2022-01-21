using JHiga.RTSEngine.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class StateMachineGraphWindow : EditorWindow
{
    private StateMachineGraphView _graphView;

    [MenuItem("RTS/State Machine Graph")]
    public static void ShowWindow()
    {
        StateMachineGraphWindow window = GetWindow<StateMachineGraphWindow>();
        window.titleContent = new GUIContent("State Machine Graph");
        window.Show();
    }
    private void OnEnable()
    {
        BuildGraph();
        BuildToolBar();
    }
    private void OnDisable()
    {
        rootVisualElement.Remove(_graphView);
    }

    private void BuildGraph()
    {
        _graphView = new StateMachineGraphView
        {
            name = "State Machine Graph"
        };
        _graphView.StretchToParentSize();
        rootVisualElement.Add(_graphView);
    }

    private void BuildToolBar()
    {
        Toolbar toolbar = new Toolbar();

        Button loadStateButton = new Button(() =>
        {
            GenericEditWindow.Show<State>(CreateStateNode);            
        });
        loadStateButton.text = "Load State";
        toolbar.Add(loadStateButton);
        rootVisualElement.Add(toolbar);
    }

    private void CreateStateNode(UnityEngine.Object o)
    {
        _graphView.CreateStateNode((State)o);
    }

}
