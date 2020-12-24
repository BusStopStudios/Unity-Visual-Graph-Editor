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
    /// Node ports for connections
    /// </summary>
    [Serializable]
    public sealed class VisualGraphPort
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
            [SerializeReference] public VisualGraphNode Node;    // Node that should contain a port based off the guid
            [SerializeReference] public VisualGraphPort port;    // This must be set in the OnEnable for the graph cannot be stored
            public string guid;               // Reference to the port that belongs to the Node
        }

        // internals
        [HideInInspector] public string Name;
        [HideInInspector] public PortDirection Direction;
        [HideInInspector] public bool CanBeRemoved = true;
        [HideInInspector] public string guid;
        [HideInInspector] [SerializeReference] public List<VisualGraphPortConnection> Connections = new List<VisualGraphPortConnection>();

        #region UNITY_EDITOR
#if UNITY_EDITOR
        // The Editor Port (easier to link)
        public object editor_port;
#endif
		#endregion

	}
}