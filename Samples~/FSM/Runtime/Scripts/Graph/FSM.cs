using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VisualGraphRuntime;

public class FSM : VisualGraphMonoBehaviour<FSMGraph>
{
    void Update()
    {
        if (InternalGraph != null)
		{
            InternalGraph.Update();
		}
    }
}
