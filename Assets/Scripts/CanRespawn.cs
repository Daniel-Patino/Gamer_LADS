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

    private void OnDestroy()
    {
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
