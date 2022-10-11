using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public enum InputType { Yes, No }

    public InputType inputType = InputType.Yes;


    public void ReadInput()
    {
        if(inputType == InputType.Yes)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        } 
        else if (inputType == InputType.No)
        {
            Application.Quit();
        }
    }

}
