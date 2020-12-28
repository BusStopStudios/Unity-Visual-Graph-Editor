using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using VisualGraphEditor;
using VisualGraphRuntime;

[CustomPortView(typeof(FSMPort))]
public class FSMPortView : VisualGraphPortView
{
    public override void CreateView(VisualGraphPort port)
    {
        FSMPort fsmPort = (FSMPort)port;

        EnumField field = new EnumField(FSMPort.State.None);
        field.SetValueWithoutNotify(fsmPort.state);
        field.RegisterValueChangedCallback<System.Enum>(evt=>
        {
            fsmPort.state = (FSMPort.State)evt.newValue;
        });
        Add(field);
    }
}