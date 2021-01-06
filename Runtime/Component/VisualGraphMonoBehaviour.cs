///-------------------------------------------------------------------------------------------------
// author: William Barry
// date: 2020
// Copyright (c) Bus Stop Studios.
///-------------------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VisualGraphRuntime;

namespace VisualGraphRuntime
{
	/// <summary>
	/// Base class for all VisualGraph MonoBehaviours. To create your own behaviour derive from this and
	/// complete the generic with what type of VisualGraph your behaviour will use
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class VisualGraphMonoBehaviour<T> : MonoBehaviour where T : VisualGraph
	{
		/// <summary>
		/// Internal list of properties.
		/// 
		/// WARNING: Changing this will have undersired results. This list is updated to reflect the properties in the Graph. The
		/// inspector will allow you to override settings in the VisualGraph
		/// </summary>
		[SerializeReference] [HideInInspector] public List<AbstractBlackboardProperty> BlackboardProperties = new List<AbstractBlackboardProperty>();

		/// <summary>
		/// Store a reference to the type of graph we want to use
		/// </summary>
		[SerializeField] private T graph;

		/// <summary>
		/// To be used at runtime and un editor during play
		/// </summary>
		public T Graph => internalGraph;
		private T internalGraph;

		/// <summary>
		/// On start clone the graph so we don't overwrite the internal SO in the editor
		/// and at runtime we have our own version
		/// </summary>
		protected virtual void Start()
		{
			if (graph == null)
			{
				Debug.LogWarning($"{name} requires a VisualGraph");
			}
			else
			{
				internalGraph = (T)graph.Clone();
				internalGraph.Init();
			}

			// Override properties in the internalGraph that have been selected in the this
			for (int i = 0; i < BlackboardProperties.Count; i++)
			{
				if (BlackboardProperties[i].overrideProperty == false) continue;

				foreach (var property in internalGraph.BlackboardProperties)
				{
					if (BlackboardProperties[i].guid == property.guid)
					{
						property.Copy(BlackboardProperties[i]);
						break;
					}
				}
			}
		}

		/// <summary>
		/// Add/Remove any properties to match the VisualGraph
		/// </summary>
		public void UpdateProperties()
		{
			RemoveMissingProperties();
			AddMissingProperties();

			// Override properties in the internalGraph that have been selected in the this
			for (int i = 0; i < BlackboardProperties.Count; i++)
			{
				if (BlackboardProperties[i].overrideProperty == true) continue;

				foreach (var property in graph.BlackboardProperties)
				{
					if (BlackboardProperties[i].guid == property.guid)
					{
						BlackboardProperties[i].Copy(property);
						break;
					}
				}
			}
		}

		private void RemoveMissingProperties()
		{
			// Go through the current list and remove any that may have been removed
			for (int i = 0; i < BlackboardProperties.Count; i++)
			{
				bool found = false;
				foreach (var property in graph.BlackboardProperties)
				{
					if (BlackboardProperties[i].guid == property.guid)
					{
						found = true;
						break;
					}
				}
				if (found == false)
				{
					BlackboardProperties.RemoveAt(i);
					i--;
				}
			}
		}

		private void AddMissingProperties()
		{
			if (graph != null)
			{
				// Add any that might be missing
				for (int i = 0; i < graph.BlackboardProperties.Count; i++)
				{
					bool found = false;
					foreach (var property in BlackboardProperties)
					{
						if (graph.BlackboardProperties[i].guid == property.guid)
						{
							found = true;
							break;
						}
					}
					if (found == false)
					{
						AbstractBlackboardProperty instance = Activator.CreateInstance(graph.BlackboardProperties[i].GetType()) as AbstractBlackboardProperty;
						instance.Copy(graph.BlackboardProperties[i]);
						BlackboardProperties.Add(instance);
					}
				}
			}
		}
	}
}