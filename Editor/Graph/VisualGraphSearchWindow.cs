///-------------------------------------------------------------------------------------------------
// author: William Barry
// date: 2020
// Copyright (c) Bus Stop Studios.
///-------------------------------------------------------------------------------------------------
using VisualGraphRuntime;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace VisualGraphEditor
{
    public class VisualGraphSearchWindow : ScriptableObject, ISearchWindowProvider
    {
        private EditorWindow window;
        private VisualGraphView graphView;
        private List<Type> nodeTypes = new List<Type>();
        private Texture2D indentationIcon;
        
        public void Configure(EditorWindow window, VisualGraphView graphView)
        {
            this.window = window;
            this.graphView = graphView;

            var result = new List<System.Type>();
            var assemblies = System.AppDomain.CurrentDomain.GetAssemblies();

            DefaultNodeTypeAttribute typeAttrib = graphView.VisualGraph.GetType().GetCustomAttribute<DefaultNodeTypeAttribute>();
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    if (typeAttrib != null && (type.IsAssignableFrom(typeAttrib.type) == true || type.IsSubclassOf(typeAttrib.type))
                        && type.IsSubclassOf(typeof(VisualGraphNode)) == true
                        && type.IsAbstract == false)
                    {
                        nodeTypes.Add(type);
                    }
                }
            }
            
            indentationIcon = new Texture2D(1,1);
            indentationIcon.SetPixel(0,0,new Color(0,0,0,0));
            indentationIcon.Apply();
        }
        
        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            var tree = new List<SearchTreeEntry>();
            tree.Add(new SearchTreeGroupEntry(new GUIContent("Create Node"), 0));
            //tree.Add(new SearchTreeGroupEntry(new GUIContent("Nodes"), 1));

            foreach (var type in nodeTypes)
            {
                string display_name = "";
                if (type.GetCustomAttribute<NodeNameAttribute>() != null)
                {
                    display_name = type.GetCustomAttribute<NodeNameAttribute>().name;
                }
                else
				{
                    display_name = type.Name;
                }

                tree.Add(new SearchTreeEntry(new GUIContent(display_name, indentationIcon))
                {
                    level = 1,
                    userData = type
                });
            }

            //tree.Add(new SearchTreeEntry(new GUIContent("Group", indentationIcon))
            //{
            //    level = 1,
            //    userData = new Group()
            //});

            return tree;
        }

        public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
        {
            var mousePosition = window.rootVisualElement.ChangeCoordinatesTo(window.rootVisualElement.parent, context.screenMousePosition - window.position.position);
            var graphMousePosition = graphView.contentViewContainer.WorldToLocal(mousePosition);
            switch (SearchTreeEntry.userData)
            {
                case Type nodeData:
					{
                        graphView.CreateNode(graphMousePosition, nodeData);
                        return true;
                    }
                //case Group group:
                //    graphView.CreateGroupBlock(graphMousePosition);
                //    return true;
            }
            return false;
        }
    }
}