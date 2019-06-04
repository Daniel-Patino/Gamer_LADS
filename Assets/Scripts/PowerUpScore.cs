using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScore : MonoBehaviour
{
    public int scoreMultiplier = 1;
    public float powerupDuration = 3;

    private UserInterfaceController userIC;

    void Start()
    {
        Debug.Log("Hello");
        userIC = GameObject.Find("GameController").GetComponent<UserInterfaceController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            userIC.scoreMultiplier(scoreMultiplier, powerupDuration);
        }    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
