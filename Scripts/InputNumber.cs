using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputNumber : MonoBehaviour
{
    public enum InputType { Digit, Cancel, Confirm }

    public InputType inputType = InputType.Digit;
    public string buttonValue;
    public TMP_InputField input;
    public string correctInput = "2022";

    public GameEvent unlockDrawer;
    private bool isUnlocked = false;

    void Start()
    {
        input.interactable = true;
    }

    public void ReadInput()
    {
        if(inputType == InputType.Digit)
        {
            if(input.text.Length < 4)
                input.text += buttonValue;
        } 
        else if (inputType == InputType.Cancel)
        {
            input.text = "";
        } 
        else
        {
            ValidatePass();
        }
    }

    void ValidatePass()
    {
        if(input.text == correctInput && !isUnlocked)
        {
            unlockDrawer.Raise();
            isUnlocked = true;
        }
        else
        {

        }

        input.text = "";
    }
}
