///-------------------------------------------------------------------------------------------------
// author: William Barry
// date: 2020
// Copyright (c) Bus Stop Studios.
///-------------------------------------------------------------------------------------------------
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using VisualGraphRuntime;
using UnityEngine;

namespace VisualGraphEditor
{
	[BlackboardPropertyType(typeof(LayerMaskBlackboardProperty), "LayerMask")]
	public class BlackboardLayerMaskPropertyView : BlackboardFieldView
	{
		public override void CreateField(BlackboardField field)
		{
			LayerMaskBlackboardProperty localProperty = (LayerMaskBlackboardProperty)property;
			CreatePropertyField<LayerMask, LayerMaskField>(field, localProperty);
		}
	}
}