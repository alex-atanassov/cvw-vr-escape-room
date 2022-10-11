using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Normal.Realtime;

public class SyncedCountdown : RealtimeComponent<DoubleModel>
{
    public float startingTime = 300f;
    public FloatVariable time;
    public GameEvent death;
    private bool isAlive = true;

    private bool hasGameStarted = false;

    private Realtime _realtime;

    void Awake()
    {
        _realtime = GetComponent<Realtime>();
        _realtime.didConnectToRoom += DidConnectToRoom;
    }

    void DidConnectToRoom(Realtime room)
    {
        if (!hasGameStarted)
        {
            hasGameStarted = true;
            SetActivationTime();

        }
    }


    private void Update()
    {
        if(model == null || model.value == 0.0)
        {
            time.RuntimeValue = startingTime;
        }
        else if (isAlive)
        {
            var rt = realtime.room.time;
            var mt = model.value;
            time.RuntimeValue = startingTime - (float)(rt - mt);
        }

        if (isAlive)
        {
            if (time.RuntimeValue < 0)
            {
                death.Raise();
                isAlive = false;
            }
        }
    }

    [Button]
    public void SetActivationTime()
    {
        model.value = realtime.room.time;
    }

    public void Stop()
    {
        isAlive = false;
    }
}
