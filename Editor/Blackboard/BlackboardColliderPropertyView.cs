///-------------------------------------------------------------------------------------------------
// author: William Barry
// date: 2020
// Copyright (c) Bus Stop Studios.
///-------------------------------------------------------------------------------------------------
using UnityEditor.Experimental.GraphView;
using VisualGraphRuntime;
using UnityEngine;

namespace VisualGraphEditor
{
    [BlackboardPropertyType(typeof(ColliderBlackboardProperty), "Collider")]
    public class BlackboardColliderPropertyView : BlackboardFieldView
    {
        public override void CreateField(BlackboardField field)
        {
            ColliderBlackboardProperty localProperty = (ColliderBlackboardProperty)property;
            CreateObjectPropertyField<Collider>(field, localProperty);
        }
    }
}