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
    [BlackboardPropertyType(typeof(Vector3IntBlackboardProperty), "Vector3Int")]
    public class BlackboardVector3IntPropertyView : BlackboardFieldView
    {
        public override void CreateField(BlackboardField field)
        {
            Vector3IntBlackboardProperty localProperty = (Vector3IntBlackboardProperty)property;
            CreatePropertyField<Vector3Int, Vector3IntField>(field, localProperty);
        }
    }
}