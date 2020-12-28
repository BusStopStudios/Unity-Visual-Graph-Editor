using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using VisualGraphEditor;
using VisualGraphRuntime;

[CustomPortView(typeof(ExamplePort))]
public sealed class ExamplePortView : VisualGraphPortView
{
    public override void CreateView(VisualGraphPort port)
    {
		Label field = new Label(port.Name);
		field.style.width = 100;
		Add(field);
	}
}