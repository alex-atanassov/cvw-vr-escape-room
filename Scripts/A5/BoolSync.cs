using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class BoolSync : RealtimeComponent<BoolModel>
{
    public BoolVariable _currentValue;
    private bool _previousValue;

    protected override void OnRealtimeModelReplaced(BoolModel previousModel, BoolModel currentModel)
    {
        if (previousModel != null)
        {
            previousModel.valueDidChange -= BoolDidChange;
        }

        if (currentModel != null)
        {
            //if (currentModel.isFreshModel)
            //    currentModel.value -= _currentValue.RuntimeValue;

            UpdateBool();

            currentModel.valueDidChange += BoolDidChange;
        }
    }

    private void BoolDidChange(BoolModel model, bool value)
    {
        UpdateBool();
    }

    private void UpdateBool()
    {
        _currentValue.RuntimeValue = model.value;
    }

    public void SetBool(bool value)
    {
        model.value = value;
    }

    public void InvertBool()
    {
        model.value = !model.value;
    }

    private void Update()
    {
        var value = _currentValue.RuntimeValue;
        if (value != _previousValue)
        {
            InvertBool();
            _previousValue = value;
        }
    }
}
