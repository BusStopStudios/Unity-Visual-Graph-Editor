///-------------------------------------------------------------------------------------------------
// author: William Barry
// date: 2020
// Copyright (c) Bus Stop Studios.
///-------------------------------------------------------------------------------------------------
using UnityEditor.Experimental.GraphView;
using VisualGraphRuntime;

namespace VisualGraphEditor
{
	[BlackboardPropertyType(typeof(MaterialBlackboardProperty), "Material")]
	public class BlackboardMaterialPropertyView : BlackboardFieldView
	{
		public override void CreateField(BlackboardField field)
		{
			MaterialBlackboardProperty localProperty = (MaterialBlackboardProperty)property;
			CreateObjectPropertyField<UnityEngine.Material>(field, localProperty);
		}
	}
}