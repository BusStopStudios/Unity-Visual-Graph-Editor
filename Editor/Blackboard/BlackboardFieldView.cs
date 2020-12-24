///-------------------------------------------------------------------------------------------------
// author: William Barry
// date: 2020
// Copyright (c) Bus Stop Studios.
///-------------------------------------------------------------------------------------------------
using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using VisualGraphRuntime;

namespace VisualGraphEditor
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class BlackboardPropertyTypeAttribute : Attribute
    {
        public Type type;
		public string menuName;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_name"></param>
		public BlackboardPropertyTypeAttribute(Type _type, string _menuName)
        {
            type = _type;
			menuName = _menuName;
		}
    }

    public abstract class BlackboardFieldView : VisualElement
	{
		public BlackboardView blackboardView;
		protected VisualGraph visualGraph;
		public AbstractBlackboardProperty property;

		public Action<BlackboardFieldView> onRemoveBlackboardProperty;

		public abstract void CreateField(BlackboardField field);

		public void CreateView(VisualGraph visualGraph, AbstractBlackboardProperty property)
		{
			this.visualGraph = visualGraph;
			this.property = property;

			VisualElement rowView = new VisualElement();
			rowView.style.flexDirection = FlexDirection.Row;

			//Type valueType = (Type)property.PropertyType; When we are no <T> type for property
			Type valueType = (Type)property.GetType().GetProperty("PropertyType").GetValue(property, null);

			var field = new BlackboardField
			{
				text = property.Name,
				typeText = valueType.Name,
				userData = property
			};
			rowView.Add(field);

			var deleteButton = new Button(() => onRemoveBlackboardProperty.Invoke(this))
			{
				text = "X"
			};
			field.Add(deleteButton);

			Add(rowView);
			CreateField(field);
		}

		public void CreatePropertyField<Ty, ElTy>(BlackboardField field, AbstractBlackboardProperty<Ty> property)
		{
			BaseField<Ty> propertyField = Activator.CreateInstance(typeof(ElTy)) as BaseField<Ty>;
			propertyField.label = "Value:";
			propertyField.bindingPath = "abstractData";
			propertyField.Bind(new SerializedObject(property));
			propertyField.ElementAt(0).style.minWidth = 50;
			var sa = new BlackboardRow(field, propertyField);
			Add(sa);
		}

		public void CreateObjectPropertyField<Ty>(BlackboardField field, AbstractBlackboardProperty<Ty> property) where Ty : UnityEngine.Object
		{
			ObjectField propertyField = new ObjectField("Value:");
			propertyField.objectType = typeof(Ty);
			propertyField.bindingPath = "abstractData";
			propertyField.Bind(new SerializedObject(property));
			propertyField.ElementAt(0).style.minWidth = 50;
			var sa = new BlackboardRow(field, propertyField);
			Add(sa);
		}
	}
}