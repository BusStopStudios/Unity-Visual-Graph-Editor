using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using VisualGraphRuntime;

namespace VisualGraphEditor
{
	[CustomEditor(typeof(VisualGraphMonoBehaviour<>), true)]
	public class VisualGraphMonoBehaviourInspector : Editor
	{
		private StyleSheet customStyleSheet;

		private void OnEnable()
		{
			customStyleSheet = Resources.Load<StyleSheet>("VisualGraphMonoBehaviourInspector");
		}

        public VisualElement CreatePropertyField<Ty, ElTy>(AbstractBlackboardProperty<Ty> property)
		{
			BaseField<Ty> propertyField = Activator.CreateInstance(typeof(ElTy)) as BaseField<Ty>;
			propertyField.label = property.Name;
			propertyField.bindingPath = "abstractData";
			propertyField.Bind(new SerializedObject(property));
			propertyField.SetEnabled(property.overrideProperty);
			propertyField.ElementAt(0).style.minWidth = 50;
			return propertyField;
		}

		public VisualElement CreateObjectPropertyField<Ty>(AbstractBlackboardProperty<Ty> property) where Ty : UnityEngine.Object
		{
			ObjectField propertyField = new ObjectField(property.Name);
			propertyField.objectType = typeof(Ty);
			propertyField.bindingPath = "abstractData";
			propertyField.Bind(new SerializedObject(property));
			propertyField.SetEnabled(property.overrideProperty);
			return propertyField;
		}

		public override VisualElement CreateInspectorGUI()
		{
			// Because everything in Components is a MonoBehaviour we can get the base type
			// If they base type is a generic of type VisualGraphMonoBehaviour<> then we can try and
			MethodInfo method = target.GetType().BaseType.GetMethod("UpdateProperties", BindingFlags.Public | BindingFlags.Instance);
			method.Invoke(target, null);

			VisualElement rootElement = new VisualElement();
			rootElement.styleSheets.Add(customStyleSheet);
			rootElement.style.flexDirection = FlexDirection.Column;

			VisualElement defaultInspector = new VisualElement();
			defaultInspector.AddToClassList("default_inspector");
            UnityEditor.Editor editor = UnityEditor.Editor.CreateEditor(target);
            IMGUIContainer inspectorIMGUI = new IMGUIContainer(() => { editor.OnInspectorGUI(); });
            defaultInspector.Add(inspectorIMGUI);
            rootElement.Add(defaultInspector);

            //ObjectField propertyField = new ObjectField("Graph");
            //propertyField.objectType = target.GetType().GetField("graph").GetValue(target).GetType();
            //propertyField.bindingPath = "graph";
            //propertyField.Bind(new SerializedObject(target));
            //propertyField.RegisterCallback<ChangeEvent<UnityEngine.Object>>( evt =>
            //{
            //    target.GetType().GetField("graph").SetValue(target, evt.newValue);
            //    MethodInfo method = target.GetType().BaseType.GetMethod("UpdateProperties", BindingFlags.Public | BindingFlags.Instance);
            //    method.Invoke(target, null);
            //});
            //rootElement.Add(propertyField);

            // Because everything in Components is a MonoBehaviour we can get the base type
            // If they base type is a generic of type VisualGraphMonoBehaviour<> then we can try and
            FieldInfo BlackboardPropertyInfo = target.GetType().BaseType.GetField("BlackboardProperties");
			List<AbstractBlackboardProperty> BlackboardProperties = BlackboardPropertyInfo.GetValue(target) as List<AbstractBlackboardProperty>;

            Label blackboardLabel = new Label($"Blackboard Properties: {BlackboardProperties.Count}") { name = "blackboardLabel" };
            rootElement.Add(blackboardLabel);

            foreach (var property in BlackboardProperties)
			{
				VisualElement blackboardProperty = new VisualElement() { name = "blackboardProperty" };
				blackboardProperty.style.flexDirection = FlexDirection.Row;

				Toggle overwriteField = new Toggle();
				overwriteField.SetValueWithoutNotify(property.overrideProperty);
				blackboardProperty.Add(overwriteField);

                //TODO: This is going to get ugly and I don't care at this time.....
                //		Should look at moving the code generation for each Inspector view into respective classes?
                //		HACK, HACK, HACKITY, HACK... so ugly need a better solution
                VisualElement fieldElement = null;
                switch (property)
                {
                    case BoolBlackboardProperty prop:
                        fieldElement = CreatePropertyField<bool, Toggle>(prop);
                        break;
                    case ColliderBlackboardProperty prop:
                        fieldElement = CreateObjectPropertyField<Collider>(prop);
                        break;
                    case ColorBlackboardProperty prop:
                        fieldElement = CreatePropertyField<Color, ColorField>(prop);
                        break;
                    case DoubleBlackboardProperty prop:
                        fieldElement = CreatePropertyField<double, DoubleField>(prop);
                        break;
                    case FloatBlackboardProperty prop:
                        fieldElement = CreatePropertyField<float, FloatField>(prop);
                        break;
                    case GameObjectBlackboardProperty prop:
                        fieldElement = CreateObjectPropertyField<GameObject>(prop);
                        break;
                    case IntBlackboardProperty prop:
                        fieldElement = CreatePropertyField<int, IntegerField>(prop);
                        break;
                    case LayerMaskBlackboardProperty prop:
                        fieldElement = CreatePropertyField<LayerMask, LayerField>(prop);
                        break;
                    case MaterialBlackboardProperty prop:
                        fieldElement = CreateObjectPropertyField<Material>(prop);
                        break;
                    case ObjectBlackboardProperty prop:
                        fieldElement = CreateObjectPropertyField<UnityEngine.Object>(prop);
                        break;
                    case RectBlackboardProperty prop:
                        fieldElement = CreatePropertyField<Rect, RectField>(prop);
                        break;
                    case RectIntBlackboardProperty prop:
                        fieldElement = CreatePropertyField<RectInt, RectIntField>(prop);
                        break;
                    case StringBlackboardProperty prop:
                        fieldElement = CreatePropertyField<string, TextField>(prop);
                        break;
                    case TransformBlackboardProperty prop:
                        fieldElement = CreateObjectPropertyField<Transform>(prop);
                        break;
                    case Vector2BlackboardProperty prop:
                        fieldElement = CreatePropertyField<Vector2, Vector2Field>(prop);
                        break;
                    case Vector2IntBlackboardProperty prop:
                        fieldElement = CreatePropertyField<Vector2Int, Vector2IntField>(prop);
                        break;
                    case Vector3BlackboardProperty prop:
                        fieldElement = CreatePropertyField<Vector3, Vector3Field>(prop);
                        break;
                    case Vector3IntBlackboardProperty prop:
                        fieldElement = CreatePropertyField<Vector3Int, Vector3IntField>(prop);
                        break;
                    case Vector4BlackboardProperty prop:
                        fieldElement = CreatePropertyField<Vector4, Vector4Field>(prop);
                        break;
                }

                blackboardProperty.Add(fieldElement);

                overwriteField.RegisterCallback<ChangeEvent<bool>>(evt =>
                {
                    Undo.RecordObject(target, "VisualGraphMonoBehaviour Changed");
                    property.overrideProperty = evt.newValue;
                    fieldElement.SetEnabled(property.overrideProperty);
                });

                rootElement.Add(blackboardProperty);
            }

			return rootElement;
		}
	}
}