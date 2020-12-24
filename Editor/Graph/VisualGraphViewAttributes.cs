using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VisualGraphEditor
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class CustomNodeViewAttribute : Attribute
    {
        public Type type;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_name"></param>
        public CustomNodeViewAttribute(Type type)
        {
            this.type = type;
        }
    }
}