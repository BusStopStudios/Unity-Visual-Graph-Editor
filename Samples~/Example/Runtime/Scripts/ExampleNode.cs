///-------------------------------------------------------------------------------------------------
// author: William Barry
// date: 2020
// Copyright (c) Bus Stop Studios.
///-------------------------------------------------------------------------------------------------
using UnityEngine;
using VisualGraphRuntime;

// Override the default node name (otherwise the name of the class is used)
[NodeName(_name: "Example Node")]
// Override the default settings for how many ports the node will handle
[NodePortAggregateAttribute(NodePortAggregateAttribute.PortAggregate.Multiple, NodePortAggregateAttribute.PortAggregate.Multiple)]
// Override the default settings for the Port Capacity
[PortCapacity(PortCapacityAttribute.Capacity.Multi, PortCapacityAttribute.Capacity.Multi)]
// Custom style sheet for your node
[CustomNodeStyle("ExampleNodeStyle")]
// Default Port to use
[DefaultPortType(typeof(ExamplePort))]
public class ExampleNode : VisualGraphNode
{
    public bool ExampleBool;
    public int ExampleInt;
    public string ExampleString;
    public float ExampleFloat;
    public Vector3 ExampleVector;
}
