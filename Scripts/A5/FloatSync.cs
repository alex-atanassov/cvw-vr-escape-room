using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class FloatSync : RealtimeComponent<FloatModel>
{
    public FloatVariable _currentValue;
    private float _previousValue;

    protected override void OnRealtimeModelReplaced(FloatModel previousModel, FloatModel currentModel)
    {
        if(previousModel != null)
        { 
            previousModel.valueDidChange -= FloatDidChange;
        }

        if(currentModel != null)
        {
            if (currentModel.isFreshModel)
                currentModel.value -= _currentValue.RuntimeValue;

            UpdateFloat();

            currentModel.valueDidChange += FloatDidChange;
        }
    }

    private void FloatDidChange(FloatModel model, float value)
    {
        UpdateFloat();
    }

    private void UpdateFloat()
    {
        _currentValue.RuntimeValue = model.value;
    }

    public void SetFloat(float value)
    {
        model.value = value;
    }

    private void Update()
    {
        var value = _currentValue.RuntimeValue;
        if(value != _previousValue)
        {
            SetFloat(value);
            _previousValue = value;
        }
    }
}
