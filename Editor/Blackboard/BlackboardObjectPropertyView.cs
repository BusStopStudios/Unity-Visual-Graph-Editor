///-------------------------------------------------------------------------------------------------
// author: William Barry
// date: 2020
// Copyright (c) Bus Stop Studios.
///-------------------------------------------------------------------------------------------------
using UnityEditor.Experimental.GraphView;
using VisualGraphRuntime;

namespace VisualGraphEditor
{
    [BlackboardPropertyType(typeof(ObjectBlackboardProperty), "Object")]
    public class BlackboardObjectPropertyView : BlackboardFieldView
    {
        public override void CreateField(BlackboardField field)
        {
            ObjectBlackboardProperty localProperty = (ObjectBlackboardProperty)property;
            CreateObjectPropertyField<UnityEngine.Object>(field, localProperty);
        }
    }
}
