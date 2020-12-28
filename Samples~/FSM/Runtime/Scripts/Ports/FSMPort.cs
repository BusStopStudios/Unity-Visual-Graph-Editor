using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VisualGraphRuntime;

[Serializable]
public class FSMPort : VisualGraphPort
{
    public enum State
    {
        None,
        On,
        Off
    };

    public State state = State.None;
}
