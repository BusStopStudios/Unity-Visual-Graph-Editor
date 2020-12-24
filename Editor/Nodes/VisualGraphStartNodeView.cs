using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using VisualGraphRuntime;

namespace VisualGraphEditor
{
    [CustomNodeView((typeof(VisualGraphStartNode)))]
    public sealed class VisualGraphStartNodeView : VisualGraphNodeView
    {
        public override bool ShowNodeProperties => false;

        public override Capabilities SetCapabilities(Capabilities capabilities)
        {
            capabilities &= ~UnityEditor.Experimental.GraphView.Capabilities.Movable;
            capabilities &= ~UnityEditor.Experimental.GraphView.Capabilities.Deletable;
            return capabilities;
        }
    }
}
