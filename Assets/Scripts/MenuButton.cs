using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class MenuButton : MonoBehaviour
{
    public enum InputType { Yes, No }

    public InputType inputType = InputType.Yes;

    public UnityEvent events;


    public void ReadInput()
    {
        if(inputType == InputType.Yes)
        {
            events.Invoke();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        } 
        else if (inputType == InputType.No)
        {
            Application.Quit();
        }
    }

}
