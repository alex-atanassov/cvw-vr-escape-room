using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Normal.Realtime;

public class Brush : MonoBehaviour {
    InputDevice RightController;
    InputDevice LeftController;

    public Transform leftHand, rightHand;
    private Transform brushHand;

    // Reference to Realtime to use to instantiate brush strokes
    [SerializeField] private Realtime _realtime = null;

    // Prefab to instantiate when we draw a new brush stroke
    [SerializeField] private GameObject _brushStrokePrefab = null;

    // Which hand should this brush instance track?
    private enum Hand { LeftHand, RightHand };
    [SerializeField] private Hand _hand = Hand.RightHand;

    // Used to keep track of the current brush tip position and the actively drawing brush stroke
    private Vector3     _handPosition;
    private Quaternion  _handRotation;
    private BrushStroke _activeBrushStroke;

    void Start()
    {
        brushHand = _hand == Hand.LeftHand ? leftHand : rightHand;

        var leftHandedControllers = new List<UnityEngine.XR.InputDevice>();
        var desiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Left | UnityEngine.XR.InputDeviceCharacteristics.Controller;
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, leftHandedControllers);

        if (leftHandedControllers.Count > 0)
            LeftController = leftHandedControllers[0];


        var rightHandedControllers = new List<UnityEngine.XR.InputDevice>();
        desiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Right | UnityEngine.XR.InputDeviceCharacteristics.Controller;
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, rightHandedControllers);

        if (rightHandedControllers.Count > 0)
            RightController = rightHandedControllers[0];
    }

    private void Update() {
        if (!_realtime.connected)
            return;

        // Start by figuring out which hand we're tracking
        XRNode node    = _hand == Hand.LeftHand ? XRNode.LeftHand : XRNode.RightHand;
        InputDevice trigger = _hand == Hand.LeftHand ? LeftController : RightController;

        // Get the position & rotation of the hand
        bool handIsTracking = UpdatePose(node, ref _handPosition, ref _handRotation, ref brushHand);

        // Figure out if the trigger is pressed or not

        bool triggerPressed = (trigger.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out bool triggerValue) && triggerValue);

        Debug.Log("Trigger pressed: " + triggerPressed);

        // If we lose tracking, stop drawing
        if (!handIsTracking)
            triggerPressed = false;

        // If the trigger is pressed and we haven't created a new brush stroke to draw, create one!
        if (triggerPressed && _activeBrushStroke == null) {
            // Instantiate a copy of the Brush Stroke prefab, set it to be owned by us.
            GameObject brushStrokeGameObject = Realtime.Instantiate(_brushStrokePrefab.name, ownedByClient: true, useInstance: _realtime);

            // Grab the BrushStroke component from it
            _activeBrushStroke = brushStrokeGameObject.GetComponent<BrushStroke>();

            // Tell the BrushStroke to begin drawing at the current brush position
            _activeBrushStroke.BeginBrushStrokeWithBrushTipPoint(_handPosition, _handRotation);
        }

        // If the trigger is pressed, and we have a brush stroke, move the brush stroke to the new brush tip position
        if (triggerPressed)
            _activeBrushStroke.MoveBrushTipToPoint(_handPosition, _handRotation);

        // If the trigger is no longer pressed, and we still have an active brush stroke, mark it as finished and clear it.
        if (!triggerPressed && _activeBrushStroke != null) {
            _activeBrushStroke.EndBrushStrokeWithBrushTipPoint(_handPosition, _handRotation);
            _activeBrushStroke = null;
        }
    }

    //// Utility

    // Given an XRNode, get the current position & rotation. If it's not tracking, don't modify the position & rotation.
    private static bool UpdatePose(XRNode node, ref Vector3 position, ref Quaternion rotation, ref Transform brushHand) {
        List<XRNodeState> nodeStates = new List<XRNodeState>();
        InputTracking.GetNodeStates(nodeStates);

        foreach (XRNodeState nodeState in nodeStates) {
            if (nodeState.nodeType == node) {
                Vector3    nodePosition;
                Quaternion nodeRotation;
                bool gotPosition = nodeState.TryGetPosition(out nodePosition);
                bool gotRotation = nodeState.TryGetRotation(out nodeRotation);

                if (gotPosition)
                    position = brushHand.position;
                if (gotRotation)
                    rotation = brushHand.rotation;

                return gotPosition;
            }
        }

        return false;
    }
}
