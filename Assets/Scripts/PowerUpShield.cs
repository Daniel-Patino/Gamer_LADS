using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpShield : MonoBehaviour
{
    public int shieldLevel = 3;
    private GameObject shieldController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && other.GetComponent<PlayerController>() != null)
        {
            shieldController = GameObject.Find("PlayerController");
            shieldController.GetComponent<PlayerShieldController>().setCurrentShieldLevel(shieldLevel);
            shieldController.GetComponent<PlayerShieldController>().shieldUpdate();
        }
    }
}
