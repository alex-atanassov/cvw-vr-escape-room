using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Animator))]
public class HandXRRig : MonoBehaviour
{
    InputDevice LeftController;
    InputDevice RightController;

    Animator animator;

    public enum Buttons { GripLeft, GripRight, TriggerLeft, TriggerRight };

    private float[] buttonTarget = { 0f, 0f, 0f, 0f };
    private float[] buttonCurrent = { 0f, 0f, 0f, 0f };

    public float speed = 1.0f;
    private string[] animatorNames = { "GripLeft", "GripRight", "TriggerLeft", "TriggerRight" };

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        // Set up XR controllers
        GetDevices();
    }

    // Update is called once per frame
    void Update()
    {
        GetXRInput();
        AnimateHand();
    }

    internal void SetButton(float v, Buttons button)
    {
        buttonTarget[(int)button] = v;
    }

    void AnimateHand()
    {
        for(int i = 0; i < 4; i++)
        {
            if (buttonCurrent[i] != buttonTarget[i])
            {
                buttonCurrent[i] = Mathf.MoveTowards(buttonCurrent[i], buttonTarget[i], Time.deltaTime * speed);
                animator.SetFloat(animatorNames[i], buttonCurrent[i]);
            }
        }
    }

    void GetXRInput()
    {
        // primary trigger events
        LeftController.TryGetFeatureValue(CommonUsages.trigger, out float leftTriggerValue);
        RightController.TryGetFeatureValue(CommonUsages.trigger, out float rightTriggerValue);

        SetButton(leftTriggerValue, Buttons.TriggerLeft);
        SetButton(rightTriggerValue, Buttons.TriggerRight);

        // grip trigger events
        LeftController.TryGetFeatureValue(CommonUsages.grip, out float leftGripValue);
        RightController.TryGetFeatureValue(CommonUsages.grip, out float rightGripValue);

        SetButton(leftGripValue, Buttons.GripLeft);
        SetButton(rightGripValue, Buttons.GripRight);
    }

    void GetDevices()
    {
        List<InputDevice> LeftControllerDevices = new List<InputDevice>();
        InputDeviceCharacteristics LeftControllerCharacteristics = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(LeftControllerCharacteristics, LeftControllerDevices);
        if (LeftControllerDevices.Count > 0) LeftController = LeftControllerDevices[0];

        List<InputDevice> RightControllerDevices = new List<InputDevice>();
        InputDeviceCharacteristics RightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(RightControllerCharacteristics, RightControllerDevices);
        if (RightControllerDevices.Count > 0) RightController = RightControllerDevices[0];
    }
}
