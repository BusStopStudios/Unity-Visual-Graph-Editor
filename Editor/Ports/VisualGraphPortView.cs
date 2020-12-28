using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using VisualGraphRuntime;

namespace VisualGraphEditor
{
    public abstract class VisualGraphPortView : VisualElement
    {
        public abstract void CreateView(VisualGraphPort port);
    }
}