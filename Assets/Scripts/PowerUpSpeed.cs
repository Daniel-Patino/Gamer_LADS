using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpeed : MonoBehaviour
{
    public int moveSpeedMultiplier = 2;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && other.GetComponent<PlayerController>() != null)
        {
            other.GetComponent<PlayerController>().moveSpeed = other.GetComponent<PlayerController>().moveSpeed * moveSpeedMultiplier;
        }
    }
}
