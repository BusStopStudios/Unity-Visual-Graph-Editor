///-------------------------------------------------------------------------------------------------
// author: William Barry
// date: 2020
// Copyright (c) Bus Stop Studios.
///-------------------------------------------------------------------------------------------------
using UnityEditor.Experimental.GraphView;
using VisualGraphRuntime;

namespace VisualGraphEditor
{
    [BlackboardPropertyType(typeof(TransformBlackboardProperty), "Transform")]
    public class BlackboardTransformPropertyView : BlackboardFieldView
    {
        public override void CreateField(BlackboardField field)
		{
            TransformBlackboardProperty localProperty = (TransformBlackboardProperty)property;
            CreateObjectPropertyField<UnityEngine.Transform>(field, localProperty);
        }
    }
}