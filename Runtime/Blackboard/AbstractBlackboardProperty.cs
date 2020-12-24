///-------------------------------------------------------------------------------------------------
// author: William Barry
// date: 2020
// Copyright (c) Bus Stop Studios.
///-------------------------------------------------------------------------------------------------
using System;
using UnityEngine;

namespace VisualGraphRuntime
{
	/// <summary>
	/// Base class for all Blackboard Properties
	/// </summary>
	[Serializable]
	public abstract class AbstractBlackboardProperty : ScriptableObject
	{
		[HideInInspector] public bool overrideProperty; // used in the component at runtime
		[HideInInspector] public string guid;
		public string Name;

		public virtual void Copy(AbstractBlackboardProperty property)
		{
			guid = property.guid;
			Name = property.Name;
		}
	}

	/// <summary>
	/// Generic base class for all Blackboard Properties that contain the value
	/// </summary>
	[Serializable]
	public abstract class AbstractBlackboardProperty<T> : AbstractBlackboardProperty
	{
		[SerializeField] [HideInInspector] public T abstractData;
		public Type PropertyType => typeof(T);

		public override void Copy(AbstractBlackboardProperty property)
		{
			base.Copy(property);

			AbstractBlackboardProperty<T> propertyT = property as AbstractBlackboardProperty<T>;
			if (propertyT != null)
			{
				abstractData = propertyT.Value;
			}
		}

		protected virtual void OnDataChanged() { }

		public virtual T Value
		{
			get => abstractData;
			set
			{
				abstractData = value;
				OnDataChanged();
			}
		}
	}
}