using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/*
 * 
 */ 
public class UserInterfaceController : MonoBehaviour
{
    public int startingWave = 1;
    public int startingLives = 3;
    public int startingBombs = 3;
    public int startingScore = 0;
    public GameObject playerObject; // Health of Player is derived from it's IndividualHealth component

    private TextMeshProUGUI currentScoreWaveText;
    private Slider currentHealthSlider;
    private RectTransform currentLives;
    private Image currentBombsAmount;

    public WaveScoreTextHandler waveTextHandler;
    private HealthSliderHandler healthSliderHandler;
    private BombsAmountHandler bombsAmountHandler;

    private void getTheComponents()
    {
        currentScoreWaveText = GameObject.Find("CurrentWave").GetComponent<TextMeshProUGUI>();
        currentHealthSlider = GameObject.Find("Health").GetComponent<Slider>();
        currentLives = GameObject.Find("Lives").GetComponent<RectTransform>();
        currentBombsAmount = GameObject.Find("Amount").GetComponentInChildren<Image>();
    }

    private void findPlayer()
    {
        if(GameObject.Find("Player") == null)
        {
            return;
        }
        healthSliderHandler.setPlayerHealth(GameObject.Find("Player").GetComponent<IndividualHealth>());
    }

    public bool launchBomb()
    {
        bool hasBombs = false;
        int currentBombs = bombsAmountHandler.getBombsRemaining();
        if (currentBombs > 0) {
            bombsAmountHandler.setBombsRemaining(--currentBombs);
            bombsAmountHandler.updateBombCountSprite();
            return hasBombs = true;
        }
        return hasBombs;
    }

    void Start()
    {
        getTheComponents();
        waveTextHandler = new WaveScoreTextHandler(currentScoreWaveText, startingWave, startingScore);
        healthSliderHandler = new HealthSliderHandler(playerObject, currentHealthSlider);
        bombsAmountHandler = new BombsAmountHandler(startingBombs, currentBombsAmount);
    }

    void Update()
    {
        findPlayer();
        healthSliderHandler.updateUIBar();
    }

    /* call a method in waveTextHandler, an instance of class WaveScoreTextHandler.
     * call method because can't call int; both "currentWave" and "currentScore" are private.
     * works because said method is actually an int: "public int getCurrentScore()".
     */
    public int ScoreCarrier()
    {
        return waveTextHandler.getCurrentScore();
    }
}

/*
 * 
 */ 
public class WaveScoreTextHandler
{
    private int currentWave;
    private int currentScore;
    private TextMeshProUGUI scoreWaveText;

    public WaveScoreTextHandler(TextMeshProUGUI scoreWaveText, int currentWave, int currentScore)
    {
        this.currentWave = currentWave;
        this.scoreWaveText = scoreWaveText;
    }

    public void setScoreWaveText()
    {
        scoreWaveText.text = "Wave: # " + currentWave + " Score: # " + currentScore;
    }

    public int getCurrentScore()
    {
        return currentScore;
    }

    public int getCurrentWave()
    {
        return currentWave; 
    }

    public void setCurrentWave(int newWave)
    {
        currentWave = newWave;
    }

    public void setCurrentScore(int newScore)
    {
        currentScore = newScore;
    }
}

/*
 * 
 */ 
public class HealthSliderHandler
{
    private int health;
    private Slider healthSlider;
    private IndividualHealth playerHealth;

    public HealthSliderHandler(GameObject playerObject, Slider healthSlider)
    {
        this.healthSlider = healthSlider;
        this.playerHealth = playerObject.GetComponent<IndividualHealth>();
        healthUISetup();
    }

    private void healthUISetup()
    {
        healthSlider.maxValue = playerHealth.startingHealth;
        healthSlider.value = playerHealth.getCurrentHealth();
    }

    public void setPlayerHealth(IndividualHealth playerHealth)
    {
        this.playerHealth = playerHealth;
    }

    public void updateUIBar()
    {
        healthSlider.value = playerHealth.getCurrentHealth();
    }
}

/*
 * The User Interface for the Lives is already being handled by the PlayerRespawnController
 * there is currently no need to implement anything here.
 */
public class LivesUIHandler { }

/*
 * The BombsAmountHandler should swap the sprite image to the corresponding image in 
 * relation to how many bombs the player has
 */
public class BombsAmountHandler {

    private int bombs;
    private int bombsRemaining;
    private Image bombCountImage;

    public BombsAmountHandler(int bombs, Image bombCountImage)
    {
        this.bombs = bombs;
        this.bombsRemaining = this.bombs;
        this.bombCountImage = bombCountImage;
        bombCountImage.sprite = Resources.Load<Sprite>("numeral" + bombsRemaining.ToString());
    }

    public int getBombsRemaining()
    {
        return bombsRemaining;
    }

    public void setBombsRemaining(int bombsLeft)
    {
        bombsRemaining = bombsLeft;
    }

    public void updateBombCountSprite()
    {
        bombCountImage.sprite = Resources.Load<Sprite>("numeral" + bombsRemaining.ToString());
    }
}