using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VisualGraphEditor;

/// <summary>
/// Example of how you can override the base view node
/// There are few properties to override, most are set through attributes so
/// you don't have to create this node.
/// 
/// By using the CustomNodeView, you will see what type runtime type this node will display
/// </summary>
[CustomNodeView((typeof(ExampleNode)))]
public sealed class ExampleNodeView : VisualGraphNodeView
{
    // For this example the ability to hide/show the properties for the node are
    // hidden. Comment this out or set it to true to see the properties
    [HideInInspector] public override bool ShowNodeProperties => true;
}
