using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using VisualGraphRuntime;

namespace VisualGraphEditor
{
    public class VisualGraphLabelPortView : VisualGraphPortView
    {
		public override void CreateView(VisualGraphPort port)
		{
			Label field = new Label(port.Name);
			Add(field);
		}
	}
}
