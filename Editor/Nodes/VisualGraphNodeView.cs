using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace VisualGraphEditor
{
    public class VisualGraphNodeView : Node
    {
        [HideInInspector] public virtual Vector2 default_size => new Vector2(200, 150);
        [HideInInspector] public virtual bool ShowNodeProperties => true;

        public virtual void DrawNode()
        {
        }

        public virtual Capabilities SetCapabilities(Capabilities capabilities)
        {
            return capabilities;
        }
    }
}
