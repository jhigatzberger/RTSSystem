using JHiga.RTSEngine.StateMachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class StateMachineNode : Node
{
    private static readonly Color borderColor = new Color(0, 0, 0, 0.3f);
    private static readonly int borderWidth = 1;
    public State state;
    public Dictionary<Transition, VisualTransition> visualTransitions = new Dictionary<Transition, VisualTransition>();
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

    private void InitActions()
    {
        actionContainer = new VisualElement();
        if (state.actions == null)
            state.actions = new Action[0];
        foreach (Action a in state.actions)
            AddAction(a);
        Button createAction = new Button(() => TypePickerWindow.Show<Action>(AddNewAction, parent: SOContainer.Get<Action>()));
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
        EditorUtility.SetDirty(state);
        AssetDatabase.SaveAssets();
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
        if (state.transitions == null)
            state.transitions = new Transition[0];
        foreach (Transition t in state.transitions)
            AddTransition(t);
        outputContainer.Add(transitionContainer);
        Button createTransition = new Button(() => TypePickerWindow.Show<Transition>(AddNewTransition, parent: SOContainer.Get<Transition>()));
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
        EditorUtility.SetDirty(state);
        AssetDatabase.SaveAssets();
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
        VisualTransition t = new VisualTransition(this, transition);

        visualTransitions.Add(transition, t);
        transitionContainer.Add(t);

        RefreshExpandedState();
        RefreshPorts();
    }

    public StateMachineNode()
    {
    }

    public string GUID;

    public string placeHolder;

    public bool entryPoint;
    public class VisualTransition : VisualElement
    {
        public Port truePort;
        public Port falsePort;
        private VisualElement container;
        private StateMachineNode node;
        private Transition transition;
        public VisualTransition(StateMachineNode node, Transition transition)
        {
            this.transition = transition;
            this.node = node;
            container = InstantiateContainer();
            Add(container);
        }
        private VisualElement InstantiateContainer()
        {
            container = new VisualElement();
            container.Add(InstantiateTitleContainer());
            container.Add(new Label(transition.decision.name));
            container.Add(InstantiatePort(transition.trueState, "True"));
            container.Add(InstantiatePort(transition.falseState, "False"));
            container.style.justifyContent = Justify.Center;
            container.style.marginBottom = 5;
            SetBorder(container, bottom: true);
            return container;
        }
        private VisualElement InstantiateTitleContainer()
        {
            VisualElement titleContainer = new VisualElement();
            titleContainer.Add(GenerateRemoveButton(() => node.RemoveTransition(transition)));
            Label transitionTitle = new Label(transition.name);
            transitionTitle.style.unityFontStyleAndWeight = FontStyle.Bold;
            titleContainer.Add(transitionTitle);
            titleContainer.style.flexDirection = FlexDirection.RowReverse;
            return titleContainer;
        }
        private Port InstantiatePort(State state, string name)
        {
            Port p = node.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(float));
            if (state != null)
            {
                if (!node.graphView.statesToNodes.TryGetValue(state, out StateMachineNode stateNode))
                    stateNode = node.graphView.CreateStateNode(state);
                if (stateNode != node)
                    LinkNodes(p, stateNode.input);
            }
            else
                LinkNodes(p, node.graphView.nullNode.input);
            p.portName = name;
            return p;
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
            node.graphView.Add(edge);
        }
    }
}

