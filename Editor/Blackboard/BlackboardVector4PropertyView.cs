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
    [BlackboardPropertyType(typeof(Vector4BlackboardProperty), "Vector4")]
    public class BlackboardVector4PropertyView : BlackboardFieldView
    {
        public override void CreateField(BlackboardField field)
        {
            Vector4BlackboardProperty localProperty = (Vector4BlackboardProperty)property;
            CreatePropertyField<Vector4, Vector4Field>(field, localProperty);
        }
    }
}