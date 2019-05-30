using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public GameObject GameOverDisplay;
    public static bool GameOverState = false;
    public PlayerRespawnController playerRespawnController;
    // note: referencing class directly here requires inheriting from MonoBehaviour.
    // inheriting MonoBehaviour has its own set of +/-. one is that it assumes attachment to GameObject.

    // public GameObject gameController;
    public UserInterfaceController userInterfaceController;

    public GameObject waveNumberText;
    public GameObject scoreNumberText;
    public int test;

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

        testText = scoreNumberText.GetComponent<Text>();
        // testText.text = gameController.GetComponent<UserInterfaceController>().scoreCarrier().ToString();
        testText.text = userInterfaceController.scoreCarrier().ToString();
    }

    private int wave;
    private int score;
    private Text testText;

    private void GameOver()
    {
        // testText = waveNumberText.GetComponent<Text>();
        // testText.text = waveScoreTextHandler.getCurrentScore().ToString();

        ///= waveScoreTextHandler.getCurrentScore();

        // waveText = waveScoreTextHandler.getCurrentScore;
        // waveNumberText.Text = waveScoreTextHandler.getCurrentScore();
    }
    

    public void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
