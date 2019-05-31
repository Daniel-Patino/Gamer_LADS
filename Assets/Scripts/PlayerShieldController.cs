using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShieldController : MonoBehaviour
{
    public GameObject playerObject;
    public int initialShieldLevel = 0;
    public GameObject[] shields;

    [HideInInspector] public int currentShieldLevel = 0;

    public void shieldUpdate()
    {
        Debug.Log("Shield Initialized");
        Debug.Log("Shield Level: " + currentShieldLevel);
        Instantiate(shields[currentShieldLevel - 1], playerObject.transform.position, Quaternion.Euler(90, 0, 0));
    }

    private void shieldPowerUp()
    {
        Debug.Log("Shield Updating 123");
    }
}
