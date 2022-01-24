using JHiga.RTSEngine.StateMachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class StateMachineNode : Node
{
    private static readonly Color borderColor = new Color(0, 0, 0, 0.3f);
    private static readonly int borderWidth = 1;
    public State state;
    private Dictionary<Transition, VisualElement> visualTransitions = new Dictionary<Transition, VisualElement>();
    private VisualElement transitionContainer;
    private Dictionary<Action, VisualElement> visualActions = new Dictionary<Action, VisualElement>();
    private VisualElement actionContainer;
    private StateMachineGraphView graphView;
    public Port input;
    public StateMachineNode(State state, StateMachineGraphView graphView)
    {
        title = state.name;
        this.state = state;
        this.graphView = graphView;

        InitInput();

        graphView.statesToNodes[state] = this;

        InitTransitions();
        InitActions();

        RefreshExpandedState();
        RefreshPorts();
    }

    public Port GenerateOutputPort(Transition transition, bool decisionResult)
    {
        Port p = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(float));
        //p.AddManipulator(new EdgeConnector<Edge>(new UpdateStateEdgeConnectorListener(decisionResult, transition)));
        return p;
    }
    private void InitActions()
    {
        actionContainer = new VisualElement();
        foreach (Action a in state.actions)
            AddAction(a);
        Button createAction = new Button(() => TypePickerWindow.Show<Action>(AddNewAction));
        createAction.text = "New Action";
        inputContainer.Add(actionContainer);
        inputContainer.Add(createAction);
    }

    private static void SetBorder(VisualElement element, bool top = false, bool bottom = false, bool left = false, bool right = false)
    {
        if (top)
        {
            element.style.borderTopWidth = borderWidth;
            element.style.borderTopColor = borderColor;
        }
        if (bottom)
        {
            element.style.borderBottomWidth = borderWidth;
            element.style.borderBottomColor = borderColor;
        }
        if (left)
        {
            element.style.borderLeftWidth = borderWidth;
            element.style.borderLeftColor = borderColor;
        }
        if (right)
        {
            element.style.borderRightWidth = borderWidth;
            element.style.borderRightColor = borderColor;
        }
    }
    private static Button GenerateRemoveButton(System.Action clickEvent)
    {
        Button removeButton = new Button(clickEvent);
        removeButton.text = "✗";
        removeButton.style.width = 20;
        removeButton.style.height = 20;
        removeButton.style.borderBottomLeftRadius = int.MaxValue;
        removeButton.style.borderBottomRightRadius = int.MaxValue;
        removeButton.style.borderTopLeftRadius = int.MaxValue;
        removeButton.style.borderTopRightRadius = int.MaxValue;
        return removeButton;
    }

    private void AddAction(Action action)
    {
        VisualElement container = new VisualElement();
        container.Add(GenerateRemoveButton(() => RemoveAction(action)));
        container.Add(new Label(action.name));
        container.style.justifyContent = Justify.Center;
        container.style.marginBottom = 5;
        container.style.flexDirection = FlexDirection.RowReverse;
        SetBorder(container, bottom: true);
        visualActions.Add(action, container);
        actionContainer.Add(container);
    }
    private void AddNewAction(Object o)
    {
        Action action = o as Action;
        if (action == null)
            return;
        List<Action> actions = new List<Action>(state.actions);
        actions.Add(action);
        state.actions = actions.ToArray();
        AddAction(action);
    }

    private void RemoveAction(Action action)
    {
        actionContainer.Remove(visualActions[action]);
        List<Action> actions = new List<Action>(state.actions);
        actions.Remove(action);
        state.actions = actions.ToArray();
    }

    private void InitInput()
    {
        input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(float));
        input.portName = "Input";
        titleContainer.Insert(0, input);
    }

    private void InitTransitions()
    {
        transitionContainer = new VisualElement();
        foreach (Transition t in state.transitions)
            AddTransition(t);
        outputContainer.Add(transitionContainer);
        Button createTransition = new Button(() => TypePickerWindow.Show<Transition>(AddNewTransition));
        createTransition.text = "New Transition";
        outputContainer.Add(createTransition);
    }

    private void AddNewTransition(Object o)
    {
        Transition transition = o as Transition;
        if (transition == null)
            return;
        List<Transition> transitions = new List<Transition>(state.transitions);
        transitions.Add(transition);
        state.transitions = transitions.ToArray();
        AddTransition(transition);
    }

    public void RemoveTransition(Transition transition)
    {
        transitionContainer.Remove(visualTransitions[transition]);
        List<Transition> transitions = new List<Transition>(state.transitions);
        transitions.Remove(transition);
        state.transitions = transitions.ToArray();
    }

    public void AddTransition(Transition transition)
    {
        
        VisualElement container = new VisualElement();
        container.style.justifyContent = Justify.Center;
        container.style.marginBottom = 5;

        SetBorder(container, bottom: true);

        VisualElement titleContainer = new VisualElement();
        titleContainer.Add(GenerateRemoveButton(() => RemoveTransition(transition)));
        Label transitionTitle = new Label(transition.name);
        transitionTitle.style.unityFontStyleAndWeight = FontStyle.Bold;
        titleContainer.Add(transitionTitle);        
        titleContainer.style.flexDirection = FlexDirection.RowReverse;
        container.Add(titleContainer);

        container.Add(new Label(transition.decision.name));

        Port trueState = GenerateOutputPort(transition, true);
        Port falseState = GenerateOutputPort(transition, false);

        trueState.portName = "True";
        falseState.portName = "False";

        container.Add(trueState);
        container.Add(falseState);

        visualTransitions.Add(transition, container);
        transitionContainer.Add(container);

        if (transition.trueState != null)
        {
            if (!graphView.statesToNodes.TryGetValue(transition.trueState, out StateMachineNode trueStateNode))
                trueStateNode = graphView.CreateStateNode(transition.trueState);
            if(trueStateNode!=this)
                LinkNodes(trueState, trueStateNode.input);
        }
        else
            LinkNodes(trueState, graphView.nullNode.input);
        if (transition.falseState != null)
        {
            if (!graphView.statesToNodes.TryGetValue(transition.falseState, out StateMachineNode falseStateNode))
                falseStateNode = graphView.CreateStateNode(transition.falseState);
            if (falseStateNode != this)
                LinkNodes(falseState, falseStateNode.input);
        }
        else
            LinkNodes(falseState, graphView.nullNode.input);

        RefreshExpandedState();
        RefreshPorts();
    }

    private void LinkNodes(Port output, Port input)
    {
        Edge edge = new Edge
        {
            output = output,
            input = input
        };
        output.Connect(edge);
        input.Connect(edge);
        graphView.Add(edge);
    }


    public StateMachineNode()
    {
    }

    public string GUID;

    public string placeHolder;

    public bool entryPoint;
}
/*
internal class UpdateStateEdgeConnectorListener : IEdgeConnectorListener
{
    private bool decisionResult;
    private Transition transition;
    public UpdateStateEdgeConnectorListener(bool decisionResult, Transition transition)
    {
        this.decisionResult = decisionResult;
        this.transition = transition;
    }

    public void OnDrop(GraphView graphView, Edge edge)
    {
        Debug.Log("drop ");
        StateMachineNode inputNode = ((StateMachineNode)edge.input.node);
        StateMachineNode outputNode = ((StateMachineNode)edge.output.node);
        if (decisionResult)
            outputNode.state.transitions.First(t => t.Equals(transition)).trueState = inputNode.state;
        else
            outputNode.state.transitions.First(t => t.Equals(transition)).falseState = inputNode.state;
    }

    public void OnDropOutsidePort(Edge edge, Vector2 position)
    {
        StateMachineNode outputNode = ((StateMachineNode)edge.output.node);
        Debug.Log("drop outside ");
        if (decisionResult)
            outputNode.state.transitions.First(t => t.Equals(transition)).trueState = outputNode.state;
    }
}*/