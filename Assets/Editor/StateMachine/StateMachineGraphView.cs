using JHiga.RTSEngine.StateMachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class StateMachineGraphView : GraphView
{

    public Dictionary<State, StateMachineNode> statesToNodes = new Dictionary< State, StateMachineNode>();
    public StateMachineNode nullNode;
    private readonly Vector2 defaultNodeSize = new Vector2(200, 200);
    public StateMachineGraphView()
    {
        styleSheets.Add(Resources.Load<StyleSheet>("StateMachineGraph"));
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        GridBackground g = new GridBackground();
        Insert(0, g);
        g.StretchToParentSize();
        nullNode = GenerateEntryPointNode();
        AddElement(nullNode);
        graphViewChanged = OnGraphChange;
    }

    private Port GeneratePort(StateMachineNode node, Direction portDirection, Port.Capacity capacity = Port.Capacity.Single)
    {
        return node.InstantiatePort(Orientation.Horizontal, portDirection, capacity, typeof(float));
    }

    private StateMachineNode GenerateEntryPointNode()
    {
        StateMachineNode node = new StateMachineNode
        {
            title = "null",
            GUID = Guid.NewGuid().ToString(),
            entryPoint = true
        };

        Port p = GeneratePort(node, Direction.Input, Port.Capacity.Multi);
        p.portName = "";
        node.inputContainer.Add(p);
        node.input = p;

        node.RefreshExpandedState();
        node.RefreshPorts();

        node.SetPosition(new Rect(100, 200, 100, 150));
        return node;
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        List<Port> compatiblePorts = new List<Port>();

        ports.ForEach((port) =>
        {
            if (startPort != port && startPort.node != port.node)
                compatiblePorts.Add(port);
        });
        return compatiblePorts;
    }
    private int createCalls = 0;
    public StateMachineNode CreateStateNode(State state)
    {
        StateMachineNode node = new StateMachineNode(state, this);
        node.SetPosition(new Rect(new Vector2(createCalls++ * (defaultNodeSize.x +100) + 200, 0), defaultNodeSize));
        AddElement(node);        
        return node;
    }

    private GraphViewChange OnGraphChange(GraphViewChange change)
    {
        if (change.edgesToCreate != null)
        {
            foreach (Edge edge in change.edgesToCreate)
            {
                Debug.Log("Implement new connection stuff");
            }
        }

        if (change.elementsToRemove != null)
        {
            foreach (GraphElement e in change.elementsToRemove)
            {
                if (e.GetType() == typeof(Edge))
                {
                    Debug.Log("Implement remove connections and states stuff");
                }
            }
        }

        if (change.movedElements != null)
        {
            foreach (GraphElement e in change.movedElements)
            {
                if (e.GetType() == typeof(Node))
                {
                    Debug.Log("Dont really care about moving (DONT FORGET TO REMOVE CONNECTION FROM TRANSITION (TRUE -> NULL IN ATTACKIDLE)");
                }
            }
        }

        return change;
    }

}