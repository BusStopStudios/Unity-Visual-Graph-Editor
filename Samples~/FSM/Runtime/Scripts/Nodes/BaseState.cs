using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VisualGraphRuntime;

[DefaultPortType(typeof(FSMPort))]

public abstract class BaseState : VisualGraphNode
{
	[HideInInspector] public FSMGraph fsm;

	public virtual void OnEnter() 
	{
		editor_ActiveNode = true;
	}
	
	public virtual void OnUpdate()
	{
	}
	
	public virtual void OnExit() 
	{ 
		editor_ActiveNode = false;
	}
}
