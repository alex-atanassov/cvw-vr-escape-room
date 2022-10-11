using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class HandController : MonoBehaviour
{
    public XRController controller;
    public HandXRRig.Buttons trigger;
    public HandXRRig.Buttons grip;
    public HandXRRig hand;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<XRController>();
    }

    // Update is called once per frame
    void Update()
    {
        controller.inputDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue);
        hand.SetButton(gripValue, grip);

        controller.inputDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
        hand.SetButton(triggerValue, trigger);
    }
}
