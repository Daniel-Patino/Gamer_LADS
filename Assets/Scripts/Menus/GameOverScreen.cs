using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public GameObject GameOverDisplay;
    public static bool GameOverState = false;
    public PlayerRespawnController playerRespawnController;
    public WaveScoreTextHandler waveScoreTextHandler;
    public GameObject waveNumberText;
    public GameObject scoreNumberText;

    // Start is called before the first frame update
    void Awake()
    {
        GameOverState = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerRespawnController.lives <= 0)
        {
            GameOverState = true;
        }

        if (GameOverState == true)
        {
            GameOver();
        }
    }

    private int wave;
    private int score;
    private void GameOver()
    {
        // waveText = waveScoreTextHandler.getCurrentScore;
    
    }
    

    public void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
