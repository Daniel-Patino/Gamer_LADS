using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    /*"If we want a variable to have the same value throughout all instances of the object then we can declare it as a static variable in our program. To manipulate and use the values of static variables we can also define a function as static.

    The keyword "static" means that only one instance of a given variable exists for a class. Static variables are used to define constants because their values can be retrieved by invoking the class without creating an instance of it."*/
    public static bool PauseState = false;
    public GameObject PauseScreen;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseState == true)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        PauseScreen.SetActive(false);
        Time.timeScale = 1f;
        PauseState = false;
    }

    void Pause()
    {
        PauseScreen.SetActive(true);
        Time.timeScale = 0f;
        PauseState = true;
    }

    public void LoadMainMenu()
    {
        Debug.Log("Loading menu...");
        // https://www.youtube.com/watch?v=JivuXdrIHK0 9:10 forward
    }

    public void EndGame()
    {
        Debug.Log("Quitting game...");
    }
}
