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
    [BlackboardPropertyType(typeof(RectIntBlackboardProperty), "RectInt")]
    public class BlackboardRectIntPropertyView : BlackboardFieldView
    {
        public override void CreateField(BlackboardField field)
		{
            RectIntBlackboardProperty localProperty = (RectIntBlackboardProperty)property;
            CreatePropertyField<RectInt, RectIntField>(field, localProperty);
		}
    }
}