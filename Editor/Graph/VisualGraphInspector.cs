///-------------------------------------------------------------------------------------------------
// author: William Barry
// date: 2020
// Copyright (c) Bus Stop Studios.
///-------------------------------------------------------------------------------------------------
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using VisualGraphRuntime;

namespace VisualGraphEditor
{
	[CustomEditor(typeof(VisualGraph), true)]
	public class VisualGraphInspector : Editor
	{
		[OnOpenAssetAttribute(1)]
		public static bool OpenVisualGraph(int instanceID, int line)
		{
			VisualGraph graph = EditorUtility.InstanceIDToObject(instanceID) as VisualGraph;
			if (graph != null)
			{
				VisualGraphEditor.CreateGraphViewWindow(graph, true);
				return true;
			}
			return false;
		}

		public override void OnInspectorGUI()
		{
			EditorGUI.EndDisabledGroup();
			DrawDefaultInspector();
			EditorGUI.EndDisabledGroup();

			if (GUILayout.Button("Reset"))
			{
				VisualGraph graph = (VisualGraph)target;

				graph.StartingNode = null;
				foreach (var node in graph.Nodes)
				{
					DestroyImmediate(node, true);
				}
				graph.Nodes = new List<VisualGraphNode>();

				//graph.Groups.Clear();
				graph.BlackboardProperties.Clear();
				
				AssetDatabase.SaveAssets();
			}
		}
	}
}