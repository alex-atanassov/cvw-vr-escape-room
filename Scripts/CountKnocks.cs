using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountKnocks : MonoBehaviour
{
    public int countKnock = 0;
    public int requiredKnocks = 3;
    public GameEvent OpenDoor;

    public void addKnock()
    {
        countKnock += 1;
        if(countKnock >= requiredKnocks)
        {
            OpenDoor.Raise();
        }
    }
}
