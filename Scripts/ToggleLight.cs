using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleLight : MonoBehaviour
{
    private Light light;
    public bool lightIsOn = true;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        light.enabled = lightIsOn;
    }

    public void Toggle()
    {
        lightIsOn = !lightIsOn;
        light.enabled = lightIsOn;
    }
}
