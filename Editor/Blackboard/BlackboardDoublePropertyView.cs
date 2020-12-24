///-------------------------------------------------------------------------------------------------
// author: William Barry
// date: 2020
// Copyright (c) Bus Stop Studios.
///-------------------------------------------------------------------------------------------------
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using VisualGraphRuntime;

namespace VisualGraphEditor
{
	[BlackboardPropertyType(typeof(DoubleBlackboardProperty), "double")]
	public class BlackboardDoublePropertyView : BlackboardFieldView
	{
		public override void CreateField(BlackboardField field)
		{
			DoubleBlackboardProperty localProperty = (DoubleBlackboardProperty)property;
			CreatePropertyField<double, DoubleField>(field, localProperty);
		}
	}
}