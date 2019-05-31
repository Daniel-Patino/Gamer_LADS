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
            Debug.Log("no lives remain");
            GameOverState = true;
        }

        if (GameOverState == true)
        {
            GameOver();
        }
        
        // currently duplicated here for visualization purposes
        scoreText = scoreNumberText.GetComponent<Text>();
        scoreText.text = userInterfaceController.waveTextHandler.getCurrentScore().ToString();

        waveText = waveNumberText.GetComponent<Text>();
        waveText.text = userInterfaceController.waveTextHandler.getCurrentWave().ToString();

    }

    private Text scoreText;
    private Text waveText;

    private int wave;
    private int score;
    private void GameOver()
    {
        // currently duplicated in Update for visualization purposes
        GameOverDisplay.SetActive(true);

        scoreText = scoreNumberText.GetComponent<Text>();
        scoreText.text = userInterfaceController.waveTextHandler.getCurrentScore().ToString();

        waveText = waveNumberText.GetComponent<Text>();
        waveText.text = userInterfaceController.waveTextHandler.getCurrentWave().ToString();
    }
    
    public void LoadMainMenu()
    {
        Debug.Log("loading main menu");
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
