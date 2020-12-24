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
    [BlackboardPropertyType(typeof(Vector2IntBlackboardProperty), "Vector2Int")]
    public class BlackboardVector2IntPropertyView : BlackboardFieldView
    {
        public override void CreateField(BlackboardField field)
        {
            Vector2IntBlackboardProperty localProperty = (Vector2IntBlackboardProperty)property;
            CreatePropertyField<Vector2Int, Vector2IntField>(field, localProperty);
        }
    }
}