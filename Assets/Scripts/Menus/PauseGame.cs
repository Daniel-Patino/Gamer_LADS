using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// for some reason this script does not work at canvas level.
public class PauseGame : MonoBehaviour
{
 /*"If we want a variable to have the same value throughout all instances of the object then we can declare it as a static variable in our program. To manipulate and use the values of static variables we can also define a function as static.

    The keyword "static" means that only one instance of a given variable exists for a class. Static variables are used to define constants because their values can be retrieved by invoking the class without creating an instance of it."*/
    public static bool PauseState = false;
    public GameObject PauseMenu;

    // when escape is pressed
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
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        PauseState = false;
    }

    void Pause()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        PauseState = true;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void EndGame()
    {
        Debug.Log("Quitting game...");
    }
}
