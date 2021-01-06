using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VisualGraphRuntime;

public class FSM : VisualGraphMonoBehaviour<FSMGraph>
{
    void Update()
    {
        if (Graph != null)
		{
            Graph.Update();
		}
    }
}
