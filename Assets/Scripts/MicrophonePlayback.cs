using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrophonePlayback : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach(var device in Microphone.devices)
        {
            Debug.Log("Name: " + device);
        }

        var audio = GetComponent<AudioSource>();
        audio.clip = Microphone.Start(Microphone.devices[0], true, 10, 44100);
        audio.loop = true;
        while (!(Microphone.GetPosition(Microphone.devices[0]) > 0)) { }

        audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
