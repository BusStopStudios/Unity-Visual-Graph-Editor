///-------------------------------------------------------------------------------------------------
// author: William Barry
// date: 2020
// Copyright (c) Bus Stop Studios.
///-------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        //[HideInInspector] [SerializeField] public List<VisualGraphGroup> Groups = new List<VisualGraphGroup>();

		/// <summary>
		/// Blackboard properties. Get/Set these through your behaviour
		/// </summary>
		[SerializeReference] [HideInInspector] public List<AbstractBlackboardProperty> BlackboardProperties = new List<AbstractBlackboardProperty>();

		/// <summary>
		/// Creates a clone of this graph (used for internal graph settings for the MonoBehaviour)
		/// </summary>
		/// <returns></returns>
		public virtual VisualGraph Clone()
		{
			VisualGraph clone = Instantiate(this);
			for(int i = 0; i < Nodes.Count; i++)
            {
				VisualGraphNode newNode = Instantiate(Nodes[i]) as VisualGraphNode;
				newNode.graph = clone;
				clone.Nodes[i] = newNode;
			}

			clone.StartingNode = clone.FindNodeByGuid(StartingNode.guid);
			clone.InitializeGraph();

			return clone;
		}

		/// <summary>
		/// Once a graph is cloned Init should be called to ensure your nodes are setup
		/// </summary>
		public virtual void Init() 
		{
		}

		/// <summary>
		/// Initialize all nodes with new version of graph
		/// </summary>
		public void InitializeGraph()
        {
			foreach (var node in Nodes)
			{
				node.graph = this;
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
		/// Adds a new node to the graph based off type
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public virtual VisualGraphNode AddNode<T>() where T : VisualGraphNode
		{
			return AddNode(typeof(T)) as T;
		}

		/// <summary>
		/// Adds a new node to the graph based off type
		/// </summary>
		/// <param name="nodeType"></param>
		/// <returns></returns>
		public virtual VisualGraphNode AddNode(Type nodeType)
        {
			VisualGraphNode graphNode = Activator.CreateInstance(nodeType) as VisualGraphNode;
			graphNode.graph = this;
			graphNode.Init();
			Nodes.Add(graphNode);

			NodePortAggregateAttribute dynamicsAttrib = graphNode.GetType().GetCustomAttribute<NodePortAggregateAttribute>();
			Debug.Assert(dynamicsAttrib != null, $"Graph node requires a NodePortAggregateAttribute {graphNode.GetType().Name}");

			PortCapacityAttribute capacityAttrib = graphNode.GetType().GetCustomAttribute<PortCapacityAttribute>();
			Debug.Assert(capacityAttrib != null, $"Graph node requires a PortCapacityAttribute {graphNode.GetType().Name}");

			if (dynamicsAttrib.InputPortAggregate != NodePortAggregateAttribute.PortAggregate.None)
			{
				VisualGraphPort graphPort = graphNode.AddPort("Input", VisualGraphPort.PortDirection.Input);
				graphPort.CanBeRemoved = false;
			}

			if (dynamicsAttrib.OutputPortAggregate == NodePortAggregateAttribute.PortAggregate.Single)
			{
				VisualGraphPort graphPort = graphNode.AddPort("Exit", VisualGraphPort.PortDirection.Output);
				graphPort.CanBeRemoved = false;
			}

			return graphNode;
		}

		/// <summary>
		/// Removes the given node from the graph
		/// </summary>
		/// <param name="graphNode"></param>
		public virtual void RemoveNode(VisualGraphNode graphNode)
		{
			graphNode.ClearConnections();
			Nodes.Remove(graphNode);
			if (Application.isPlaying)
			{
				Destroy(graphNode);
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