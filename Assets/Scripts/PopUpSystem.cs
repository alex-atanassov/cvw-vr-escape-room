using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpSystem : MonoBehaviour
{
    public GameObject popUpBox;
    private Animator animator;
    public TMP_Text popUpText;

    // Zero if not timed
    public float timer = 0f;
    private float elapsed = 0;

    //void Start()
    //{
    //    animator = GetComponent<Animator>();
    //}

    void OnEnable()
    {
        PopUp();
    }

    public void PopUp()
    {
        Debug.Log("Popup triggered");
        GetComponent<Animator>().SetTrigger("pop");
    }

    public void Close()
    {
        Debug.Log("Popup closed");
        GetComponent<Animator>().SetTrigger("close");
    }

    public void Reset()
    {
        gameObject.SetActive(false);
    }
}
