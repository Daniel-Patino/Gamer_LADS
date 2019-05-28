using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script will allow the object it is attached to the ability to Instantiate itself
 * at a given location
 */ 
public class CanRespawn : MonoBehaviour
{
    public float respawnWait = 0f;

    private PlayerRespawnController respawnController;
    private bool isQuitting = false;

    private void Awake()
    {
        isQuitting = false;
    }

    public void Start()
    {
        if (respawnController == null)
        {
            respawnController = GameObject.Find("PlayerController").GetComponent<PlayerRespawnController>();
        }
    }

    private void OnApplicationQuit()
    {
        isQuitting = true;
    }
    
    // this doesn't work because it's never actually called. OnDestroy however is auto-called upon scene change.
    /*
    private void ExitScene()
    {
        if (!PauseGame.InGameState)
        {
            Debug.Log("1.1");
            isQuitting = true;
        }
    }
    */

    private void OnDestroy()
    {
        if (!PauseGame.InGameState)
        {
            isQuitting = true;
        }

        if (!isQuitting)
        {
            Vector3 playerRespawnPosition = respawnController.playerRespawnPoint.transform.position;
            Quaternion playerRespawnRotation = respawnController.playerRespawnPoint.transform.rotation;
            respawnController.removeLife();
            if (respawnController.livesRemaining > 0)
            {
                Instantiate(respawnController.playerObject, playerRespawnPosition, playerRespawnRotation);
            }
        }
    }
}
