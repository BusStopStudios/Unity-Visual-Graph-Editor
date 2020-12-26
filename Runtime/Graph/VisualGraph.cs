///-------------------------------------------------------------------------------------------------
// author: William Barry
// date: 2020
// Copyright (c) Bus Stop Studios.
///-------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
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
			clone.InitializeConnections();
			return clone;
		}

		/// <summary>
		/// Once a graph is cloned Init should be called to ensure your nodes are setup
		/// </summary>
		public virtual void Init() 
		{
		}

		/// <summary>
		/// 
		/// </summary>
		public void InitializeConnections()
        {
			foreach (var node in Nodes)
			{
				foreach (var port in node.Ports)
				{
					foreach (var connection in port.Connections)
					{
						if (connection.initialized == false && string.IsNullOrEmpty(connection.node_guid) == false)
						{
							VisualGraphNode otherNode = FindNodeByGuid(connection.node_guid);
							if (otherNode != null)
							{
								VisualGraphPort otherPort = otherNode.FindPortByGuid(connection.port_guid);
								if (otherPort != null)
								{
									VisualGraphPort.VisualGraphPortConnection otherConnection = otherPort.FindConnectionByNodeGuid(node.guid);
									if (otherConnection != null)
									{
										otherConnection.initialized = true;
										otherConnection.Node = node;
										otherConnection.port = port;

										connection.initialized = true;
										connection.Node = otherNode;
										connection.port = otherPort;
									}
								}
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// Find a graph node based off guid
		/// </summary>
		/// <param name="guid"></param>
		/// <returns></returns>
		public VisualGraphNode FindNodeByGuid(string guid)
		{
			return Nodes.Where(n => n.guid.Equals(guid) == true).FirstOrDefault();
		}

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