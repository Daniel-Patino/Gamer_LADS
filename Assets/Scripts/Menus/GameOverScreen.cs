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
    public GameObject userInterfacePanel;
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
        if (playerRespawnController.livesRemaining <= 0)
        {
            GameOverState = true;
        }

        if (GameOverState == true)
        {
            GameOver();
        }
    }

    private Text scoreText;
    private Text waveText;

    private int wave;
    private int score;
    private void GameOver()
    {
        userInterfacePanel.SetActive(false);
        GameOverDisplay.SetActive(true);

        scoreText = scoreNumberText.GetComponent<Text>();
        scoreText.text = userInterfaceController.waveTextHandler.getCurrentScore().ToString();

        waveText = waveNumberText.GetComponent<Text>();
        waveText.text = userInterfaceController.waveTextHandler.getCurrentWave().ToString();
    }
    
    public void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
