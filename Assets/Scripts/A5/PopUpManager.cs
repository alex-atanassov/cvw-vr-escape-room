using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class PopUpManager : MonoBehaviour
{
    public GameObject deathCanvas;
    public GameObject winCanvas;

    public bool gameOver = false;

    public UnityEvent dieEvents;
    public UnityEvent winEvents;
    public UnityEvent restartEvents;
 
    public void Die()
    {
        if(!gameOver)
        {
            Debug.Log("Die");
            //deathCanvas.SetActive(true);
            dieEvents.Invoke();
        }
        gameOver = true;
    }

    public void Win()
    {
        if (!gameOver)
        {
            Debug.Log("Win");
            //winCanvas.SetActive(true);
            winEvents.Invoke();
        }
        gameOver = true;
    }

    public void Restart()
    {
        if (gameOver)
        {
            Debug.Log("Restart");
            //winCanvas.SetActive(false);
            //deathCanvas.SetActive(false);
            restartEvents.Invoke();
        }
        gameOver = false;
    }
}
