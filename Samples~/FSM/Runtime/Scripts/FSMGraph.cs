using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VisualGraphRuntime;

[CreateAssetMenu]
[DefaultNodeType(typeof(BaseState))]
public class FSMGraph : VisualGraph
{
	public BaseState currentState;

	public override void Init()
	{
		Debug.Assert(StartingNode.Outputs.First().Connections.Count != 0, "Starting node needs a connection");
		currentState = (BaseState)StartingNode.Outputs.First().Connections[0].Node;

		foreach(var state in Nodes)
		{
			BaseState fsmState = state as BaseState;
			if (fsmState != null)
			{
				fsmState.fsm = this;
			}
		}

		currentState.OnEnter();
	}

	public void Update()
	{
		if (currentState != null)
		{
			currentState.OnUpdate();
		}
	}

	public void GoToState(string transition)
	{
		if (currentState != null)
		{
			foreach (var port in currentState.Outputs)
			{
				if (port.Name == transition)
				{
					// We assume only one connection based off settings
					if (port.Connections.Count >= 0)
					{
						currentState.OnExit();
						currentState = (BaseState)port.Connections[0].Node;
						currentState.OnEnter();
					}
				}
			}
		}

	}
}
