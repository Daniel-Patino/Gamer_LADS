using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script will allow the controller to keep track of the PlayerObject and reset it upon destruction
 */ 
public class PlayerRespawnController : MonoBehaviour
{
    public Transform playerRespawnPoint;
    public GameObject playerObject;
    public int lives = 1;
    [HideInInspector] public int livesRemaining;

    private float lifeSize;
    private float currentLifeSize;
    private Vector2 originalRectSize;
    private RectTransform livesImageTransform;

    private void Start()
    {
        findUI();
        livesRemaining = lives;
        currentLifeSize = livesImageTransform.rect.width;
        lifeSize = livesImageTransform.rect.width / lives;
        originalRectSize = livesImageTransform.sizeDelta;
    }

    private void findUI()
    {
        livesImageTransform = GameObject.Find("Lives").GetComponent<RectTransform>();
    }

    public void removeLife()
    {
        if (currentLifeSize > 0)
        {
            livesRemaining--;
            currentLifeSize -= lifeSize;
            livesImageTransform.sizeDelta = new Vector2(currentLifeSize, originalRectSize.y);
        }
    }
}
