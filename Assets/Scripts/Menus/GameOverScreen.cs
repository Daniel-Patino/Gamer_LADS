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
            GameOverState = true;
        }

        if (GameOverState == true)
        {
            GameOver();
        }

        testText = scoreNumberText.GetComponent<Text>();
        // Debug.Log(gameController.GetComponent<UserInterfaceController>().scoreCarrier());
        // testText.text = gameController.GetComponent<UserInterfaceController>().scoreCarrier().ToString();
        testText.text = userInterfaceController.ScoreCarrier().ToString();

        // Debug.Log(gameController.GetComponent<UserInterfaceController>().scoreCarrier());
    }

    private Text testText;

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
