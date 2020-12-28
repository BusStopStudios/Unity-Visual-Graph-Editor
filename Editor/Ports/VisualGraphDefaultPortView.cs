using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using VisualGraphRuntime;
using UnityEditor;

namespace VisualGraphEditor
{
	[CustomPortView(typeof(VisualGraphPort))]
	public sealed class VisualGraphDefaultPortView : VisualGraphPortView
    {
        public override void CreateView(VisualGraphPort port)
        {
			TextField leftField = new TextField();
			leftField.value = port.Name;
			leftField.style.width = 100;
			leftField.RegisterCallback<ChangeEvent<string>>(
				(evt) =>
				{
					if (string.IsNullOrEmpty(evt.newValue) == false)
					{
						port.Name = evt.newValue;
					}
				}
			);
			Add(leftField);
		}
	}
}
