using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByShield : MonoBehaviour
{
    public GameObject playerController;
    private MeteorSplitter meteorSplitter;
    private PlayerShieldController playerShieldController;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Boundary")
        {
            playerController = GameObject.Find("PlayerController");
            playerShieldController = playerController.GetComponent<PlayerShieldController>();

            if (other.tag != "Player")
            {    
                playerShieldController.decrementCurrentShieldLevel(1);

                playerShieldController.shieldUpdate();

                if (other.gameObject.GetComponent<MeteorSplitter>() != null)
                {
                    other.gameObject.GetComponent<MeteorSplitter>().collideWithShield();
                    Destroy(other.gameObject);
                }
                Destroy(this.gameObject);
            }
            else
            {
                if (playerShieldController.getCurrentShieldLevel() < 4 && other.name == "ShieldBoost (1)")
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
