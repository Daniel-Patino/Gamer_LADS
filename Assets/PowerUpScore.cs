using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScore : MonoBehaviour
{
    public float scoreMultiplier = 1.5f;
    public float powerupDuration = 10;

    void Start()
    {
        StartCoroutine(activePowerUp());    
    }

    IEnumerator activePowerUp()
    {

        yield return new WaitForSeconds(powerupDuration);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
