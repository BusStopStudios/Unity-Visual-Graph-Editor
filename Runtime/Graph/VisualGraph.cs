///-------------------------------------------------------------------------------------------------
// author: William Barry
// date: 2020
// Copyright (c) Bus Stop Studios.
///-------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using UnityEngine;

namespace VisualGraphRuntime
{
	/// <summary>
	/// Base class for all VisualGraphs. Derive from this to create your own Graph
	/// </summary>
    [Serializable]
    [GraphOrientation()]
    public abstract class VisualGraph : ScriptableObject
    {
		/// <summary>
		/// Starting node can be found in the Nodes list as well
		/// </summary>
		[HideInInspector] public VisualGraphNode StartingNode;

        /// <summary>
        /// Internal list of all Nodes
        /// </summary>
        [HideInInspector] public List<VisualGraphNode> Nodes = new List<VisualGraphNode>();

		/// <summary>
		/// Comment Blocks. Not supported at this time
		/// </summary>
		[HideInInspector] public List<CommentBlock> CommentBlocks = new List<CommentBlock>();

		/// <summary>
		/// Blackboard properties. Get/Set these through your behaviour
		/// </summary>
		[SerializeReference] public List<AbstractBlackboardProperty> BlackboardProperties = new List<AbstractBlackboardProperty>();

		/// <summary>
		/// Creates a clone of this graph (used for internal graph settings for the MonoBehaviour)
		/// </summary>
		/// <returns></returns>
		public virtual VisualGraph Clone()
		{
            VisualGraph clone = Instantiate(this);
            return clone;
		}

		/// <summary>
		/// Once a graph is cloned Init should be called to ensure your nodes are setup
		/// </summary>
		public virtual void Init() { }

		/// <summary>
		/// Get a Value from the Blackboard based off a string and return true otherwise return false
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="propertyName"></param>
		/// <returns></returns>
        public bool GetPropertyValue<T>(string propertyName, ref T value)
        {
            for (int i = 0; i < BlackboardProperties.Count; i++)
            {
                if (BlackboardProperties[i].Name == propertyName)
                {
					AbstractBlackboardProperty<T> prop = (AbstractBlackboardProperty<T>)BlackboardProperties[i];
					if (prop != null)
					{
						value = prop.Value;
						return true;
					}
				}
            }
            Debug.LogWarning($"Unable to find property {propertyName}");
            return false;
        }
	}
}