using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoolListener : MonoBehaviour
{
    public BoolVariable boolVariable;
    private bool _previousValue = false;

    [System.Serializable]

    public class OnValueChanged : UnityEvent<bool> { };
    public OnValueChanged onValueChanged;

    private void Update()
    {
        var value = boolVariable.RuntimeValue;
        if (value != _previousValue)
        {
            onValueChanged.Invoke(value);
            _previousValue = value;
        }
    }
}
