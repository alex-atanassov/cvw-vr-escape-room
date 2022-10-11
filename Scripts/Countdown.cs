using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    public float startingTime = 300f;
    public FloatVariable time;
    public GameEvent death;
    private bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        time.RuntimeValue = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(isAlive)
        {
            time.RuntimeValue -= Time.deltaTime;

            if (time.RuntimeValue < 0)
            {
                death.Raise();
                isAlive = false;
            }
        }
    }

    public void Stop()
    {
        isAlive = false;
    }
}
