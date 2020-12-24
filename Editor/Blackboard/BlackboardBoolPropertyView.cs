///-------------------------------------------------------------------------------------------------
// author: William Barry
// date: 2020
// Copyright (c) Bus Stop Studios.
///-------------------------------------------------------------------------------------------------
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using VisualGraphRuntime;

namespace VisualGraphEditor
{
	[BlackboardPropertyType(typeof(BoolBlackboardProperty), "bool")]
	public class BlackboardBoolPropertyView : BlackboardFieldView
	{
		public override void CreateField(BlackboardField field)
		{
			BoolBlackboardProperty localProperty = (BoolBlackboardProperty)property;
			CreatePropertyField<bool, Toggle>(field, localProperty);
		}
	}
}