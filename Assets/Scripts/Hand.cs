using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Hand : MonoBehaviour
{
    Animator animator;

    public enum Buttons { GripLeft, GripRight, TriggerLeft, TriggerRight };

    private float[] buttonTarget = { 0f, 0f, 0f, 0f };
    private float[] buttonCurrent = { 0f, 0f, 0f, 0f };

    public float speed = 1.0f;
    private string[] animatorNames = { "GripLeft", "GripRight", "TriggerLeft", "TriggerRight" };

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimateHand();
    }

    internal void SetButton(float v, Buttons button)
    {
        buttonTarget[(int)button] = v;
    }

    void AnimateHand()
    {
        for(int i = 0; i < 4; i++)
        {
            if (buttonCurrent[i] != buttonTarget[i])
            {
                buttonCurrent[i] = Mathf.MoveTowards(buttonCurrent[i], buttonTarget[i], Time.deltaTime * speed);
                animator.SetFloat(animatorNames[i], buttonCurrent[i]);
            }
        }
    }
}
