using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
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

    public override void DrawNode()
    {
        base.DrawNode();

        VisualElement node_data = new VisualElement();
        node_data.style.backgroundColor = Color.blue;
        mainContainer.Add(node_data);

        Label example = new Label("Custom Example");
        node_data.Add(example);

        mainContainer.Add(node_data);
    }
}
