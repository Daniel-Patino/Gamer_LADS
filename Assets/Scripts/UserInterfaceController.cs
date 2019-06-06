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
    public GameObject userInterfacePanel;
    public int startingWave = 1;
    public int startingLives = 3;
    public int startingBombs = 3;
    public int startingScore = 0;
    public GameObject playerObject; // Health of Player is derived from it's IndividualHealth component

    private TextMeshProUGUI currentScoreWaveText;
    private TextMeshProUGUI scoreBoost;
    private Slider currentHealthSlider;
    private RectTransform currentLives;
    private Image currentBombsAmount;
    private GameObject scoreBoostGO;

    public WaveScoreTextHandler waveTextHandler;
    private HealthSliderHandler healthSliderHandler;
    private BombsAmountHandler bombsAmountHandler;
    private ScoreBoostHandler scoreBoostHandler;

    private float scoreBoostTimeLeft = 0f;

    void Awake()
    {
        userInterfacePanel.SetActive(true);
    }

    private void getTheComponents()
    {
        currentScoreWaveText = GameObject.Find("CurrentWave").GetComponent<TextMeshProUGUI>();
        currentHealthSlider = GameObject.Find("Health").GetComponent<Slider>();
        currentLives = GameObject.Find("Lives").GetComponent<RectTransform>();
        currentBombsAmount = GameObject.Find("Amount").GetComponentInChildren<Image>();
        scoreBoost = GameObject.Find("ScoreBoostGUI").GetComponent<TextMeshProUGUI>();
        // scoreBoostGO = GameObject.Find("ScoreBoostGUI");
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

    public void scoreMultiplier(int scoreMultiplier, float powerUpDuration)
    {
        StartCoroutine(activatedScoreMultiplier(scoreMultiplier, powerUpDuration));
        StartCoroutine(scoreBoostHandler.startScoreBoostTimer(powerUpDuration));
    }

    IEnumerator activatedScoreMultiplier(int scoreMultiplier, float powerUpDuration)
    {
        scoreBoostTimeLeft = scoreBoostTimeLeft + powerUpDuration;
        Debug.Log("Score_Multiplier_Active");
        waveTextHandler.setScoreMultiplier(scoreMultiplier);
        scoreBoostHandler.activateText(powerUpDuration);
        yield return new WaitForSeconds(powerUpDuration);
        Debug.Log("Score_Multiplier_Deactive");
        scoreBoostHandler.deactivateText();
        waveTextHandler.setScoreMultiplier(1);
    }

    void Start()
    {
        getTheComponents();
        waveTextHandler = new WaveScoreTextHandler(currentScoreWaveText, startingWave, startingScore);
        healthSliderHandler = new HealthSliderHandler(playerObject, currentHealthSlider);
        bombsAmountHandler = new BombsAmountHandler(startingBombs, currentBombsAmount);
        scoreBoostHandler = new ScoreBoostHandler(scoreBoost);
    }

    void Update()
    {
        findPlayer();
        healthSliderHandler.updateUIBar();
        if (scoreBoostHandler.isActive())
        {
            scoreBoostHandler.startScoreBoostTimer(scoreBoostTimeLeft);
        }
    }
}

/*
 * 
 */ 
public class WaveScoreTextHandler
{
    private int currentWave;
    private int currentScore;
    private int scoreMultiplier = 1;
    private TextMeshProUGUI scoreWaveText;

    public WaveScoreTextHandler(TextMeshProUGUI scoreWaveText, int currentWave, int currentScore)
    {
        this.currentWave = currentWave;
        this.scoreWaveText = scoreWaveText;
    }
    public void addToCurrentScore(int scoreValue)
    {
        currentScore = currentScore + (scoreValue * scoreMultiplier);
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
        currentScore = newScore * scoreMultiplier;
    }

    public void setScoreMultiplier(int newScoreMultiplier)
    {
        scoreMultiplier = newScoreMultiplier;
    }

    public float getScoreMultiplier()
    {
        return scoreMultiplier;
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

public class ScoreBoostHandler
{
    private int duration;
    private TextMeshProUGUI scoreBoost;
    private bool active;

    public ScoreBoostHandler(TextMeshProUGUI scoreBoost)
    {
        this.scoreBoost = scoreBoost;
    }

    public void activateText(float timeLeft)
    {
        scoreBoost.SetText("SCORE BOOST ENABLED " + timeLeft);
        active = true;
    }

    public void deactivateText()
    {
        scoreBoost.SetText("");
        scoreBoost.GetComponentInParent<Animator>().SetBool("active", true);
        active = false;
    }

    public IEnumerator startScoreBoostTimer(float timeLeft)
    {
        while (timeLeft > 0 && active == true)
        {
            timeLeft -= 1;
            setText("SCORE BOOST ENABLED " + timeLeft);
            yield return new WaitForSeconds(1);
        }
            deactivateText();
    }

    public void setText(string text)
    {
        scoreBoost.SetText(text);
    }

    public bool isActive()
    {
        return active;
    }
}