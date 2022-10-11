using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerVolume : MonoBehaviour
{
    public GameEvent win;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Win");
        win.Raise();
    }
}
