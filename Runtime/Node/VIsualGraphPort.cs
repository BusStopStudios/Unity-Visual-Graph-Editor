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
    /// Node ports for connections
    /// </summary>
    [Serializable]
    public class VisualGraphPort
    {
        /// <summary>
        /// Port Directions
        /// </summary>
        public enum PortDirection
        {
            Input,
            Output
        };

        /// <summary>
        /// Port Connections
        /// </summary>
        [Serializable]
        public class VisualGraphPortConnection
        {
            [NonSerialized] public bool initialized;
            [NonSerialized] public VisualGraphNode Node;   // Node that should contain a port based off the guid
            [NonSerialized] public VisualGraphPort port;   // This must be set in the OnEnable for the graph cannot be stored
 
            public string node_guid;                       // Reference to the port that belongs to the Node
            public string port_guid;                       // Reference to the port that belongs to the Node
        }

        // internals
        [HideInInspector] public string Name;
        [HideInInspector] public PortDirection Direction;
        [HideInInspector] public bool CanBeRemoved = true;
        [HideInInspector] public string guid;

        /// <summary>
        /// List of all connections for this port.
        /// </summary>
        [HideInInspector][SerializeField] public List<VisualGraphPortConnection> Connections = new List<VisualGraphPortConnection>();

        /// <summary>
        /// Initialize
        /// </summary>
        public virtual void Init() { }

        /// <summary>
        /// Finds a Connection by port guid
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public VisualGraphPortConnection FindConnectionByPortGuid(string guid)
        {
            return Connections.Where(c => c.port_guid.Equals(guid) == true).FirstOrDefault();
        }

        /// <summary>
        /// Finds a Connection by node guid
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public VisualGraphPortConnection FindConnectionByNodeGuid(string guid)
        {
            return Connections.Where(c => c.node_guid.Equals(guid) == true).FirstOrDefault();
        }

        /// <summary>
        /// Removes the port based off guid
        /// </summary>
        /// <param name="guid"></param>
        public void RemoveConnectionByPortGuid(string guid)
        {
            VisualGraphPortConnection connection = Connections.Where(p => p.port_guid.Equals(guid) == true).FirstOrDefault();
            if (connection != null)
            {
                Connections.Remove(connection);
            }
        }

        /// <summary>
        /// Remove all connections the port contains
        /// </summary>
        public void ClearConnections()
        {
            foreach(VisualGraphPortConnection connection in Connections)
            {
                if (connection.port != null)
                {
                    connection.port.RemoveConnectionByPortGuid(guid);
                }
            }
            Connections.Clear();
        }

        #region UNITY_EDITOR
#if UNITY_EDITOR
        // The Editor Port (easier to link)
        public object editor_port;
#endif
		#endregion

	}
}