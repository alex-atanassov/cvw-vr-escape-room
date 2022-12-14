using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoolListener : MonoBehaviour
{
    public BoolVariable boolVariable;
    public bool _previousValue = false;

    [System.Serializable]

    public class OnValueChanged : UnityEvent<bool> { };
    public OnValueChanged onValueChanged;
    //public OnValueChanged onValueTrue;
    //public OnValueChanged onValueFalse;

    void Start()
    {
        //var value = boolVariable.RuntimeValue;
        //onValueChanged.Invoke(value);
        //_previousValue = value;

        _previousValue = boolVariable.RuntimeValue;
    }

    private void Update()
    {
        var value = boolVariable.RuntimeValue;
        if (value != _previousValue)
        {
            onValueChanged.Invoke(value);
            _previousValue = value;

            //if(value) 
            //    onValueTrue.Invoke(value); 
            //else
            //    onValueFalse.Invoke(value);
        }
    }
}
