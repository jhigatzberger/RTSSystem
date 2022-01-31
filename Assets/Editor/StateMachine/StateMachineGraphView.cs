using JHiga.RTSEngine.StateMachine;
using System;
using System.Collections.Generic;
using UnityEditor;
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
        if (change.elementsToRemove != null)
        {
            foreach (GraphElement e in change.elementsToRemove)
            {
                Edge edge = e as Edge;
                if (edge != null)
                {
                    StateMachineNode outputNode = ((StateMachineNode)edge.output.node);
                    foreach (var kvp in outputNode.visualTransitions)
                    {
                        if (kvp.Value.truePort == edge.output)
                        {
                            kvp.Key.trueState = outputNode.state;
                            break;
                        }
                        if (kvp.Value.falsePort == edge.output)
                        {
                            kvp.Key.falseState = outputNode.state;
                            break;
                        }
                    }
                }
            }
        }
        if (change.edgesToCreate != null)
        {
            foreach (Edge edge in change.edgesToCreate)
            {
                StateMachineNode inputNode = ((StateMachineNode)edge.input.node);
                StateMachineNode outputNode = ((StateMachineNode)edge.output.node);
                foreach (var kvp in outputNode.visualTransitions)
                {
                    if (kvp.Value.truePort == edge.output)
                    {
                        kvp.Key.trueState = inputNode.state;
                        break;
                    }
                    if (kvp.Value.falsePort == edge.output)
                    {
                        kvp.Key.falseState = inputNode.state;
                        break;
                    }
                }
            }
        }
        AssetDatabase.SaveAssets();
        return change;
    }
}
