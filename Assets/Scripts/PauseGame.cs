using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// for some reason this script does not work at canvas level.
public class PauseGame : MonoBehaviour
{
 /*"If we want a variable to have the same value throughout all instances of the object then we can declare it as a static variable in our program. To manipulate and use the values of static variables we can also define a function as static.

    The keyword "static" means that only one instance of a given variable exists for a class. Static variables are used to define constants because their values can be retrieved by invoking the class without creating an instance of it."*/
    public static bool PauseState = false;
    public GameObject PauseMenu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("1");
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
        Debug.Log("2");
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        PauseState = false;
    }

    void Pause()
    {
        Debug.Log("3");
        PauseMenu.SetActive(true);
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
