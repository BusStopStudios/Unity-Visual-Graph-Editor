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
    [BlackboardPropertyType(typeof(RectBlackboardProperty), "Rect")]
    public class BlackboardRectPropertyView : BlackboardFieldView
    {
        public override void CreateField(BlackboardField field)
		{
            RectBlackboardProperty localProperty = (RectBlackboardProperty)property;
            CreatePropertyField<Rect, RectField>(field, localProperty);
		}
    }
}