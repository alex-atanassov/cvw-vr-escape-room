using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Normal.Realtime;

public class XRMultiplayer : MonoBehaviour
{
    public void RequestOwnership(SelectEnterEventArgs args)
    {
        var xRBaseInteractable = args.interactable;
        var realtimeView = xRBaseInteractable.GetComponent<RealtimeView>();
        var realtimeTransform = xRBaseInteractable.GetComponent<RealtimeTransform>();

        if(realtimeView != null)
        {
            realtimeView.RequestOwnership();
        } else
        {
            Debug.LogWarning("No realtimeView found");
        }

        if(realtimeTransform != null)
        {
            realtimeTransform.RequestOwnership();
        } 
        else
        {
            Debug.LogWarning("No realtimeTransform found");
        }
    }
}
