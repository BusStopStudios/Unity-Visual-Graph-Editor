///-------------------------------------------------------------------------------------------------
// author: William Barry
// date: 2020
// Copyright (c) Bus Stop Studios.
///-------------------------------------------------------------------------------------------------
using UnityEditor.UIElements;
using UnityEditor.Experimental.GraphView;
using VisualGraphRuntime;
using UnityEngine;

namespace VisualGraphEditor
{
    [BlackboardPropertyType(typeof(ColorBlackboardProperty), "Color")]
    public class BlackboardColorPropertyView : BlackboardFieldView
    {
        public override void CreateField(BlackboardField field)
        {
            ColorBlackboardProperty localProperty = (ColorBlackboardProperty)property;
            CreatePropertyField<Color, ColorField>(field, localProperty);
        }
    }
}