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
            if(other.tag != "Player")
            {
                playerController = GameObject.Find("PlayerController");
                playerShieldController = playerController.GetComponent<PlayerShieldController>();
                playerShieldController.decrementCurrentShieldLevel(1);

                Debug.Log("Calling");
                playerShieldController.shieldUpdate();

                Destroy(this.gameObject);
                if(other.gameObject.GetComponent<MeteorSplitter>() != null)
                {
                    other.gameObject.GetComponent<MeteorSplitter>().collideWithShield();
                    Destroy(other.gameObject);
                }
            }
        }
    }
}
