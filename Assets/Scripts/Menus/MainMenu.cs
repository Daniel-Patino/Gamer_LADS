using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("MyFirstGame");
        // https://www.youtube.com/watch?v=zc8ac_qUXQY 9:05
        // unity docs reccomend LoadSceneAsync over LoadScene.
    }

    public void EndGame()
    {
        Application.Quit();
        Debug.Log("Quitting game...");
    }

}
