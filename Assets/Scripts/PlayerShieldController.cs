using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShieldController : MonoBehaviour
{
    public GameObject playerObject;
    public int initialShieldLevel = 0;
    public GameObject[] shields;

    [HideInInspector] private int currentShieldLevel = 0;

    public void shieldUpdate()
    {
        Debug.Log("Shield Initialized");
        Debug.Log("Shield Level: " + currentShieldLevel);

        if(currentShieldLevel > shields.Length)
        {
            Debug.Log("Warning: Shield Level is greater than array of Shields... Shield Value: " + currentShieldLevel);
            Debug.Log("Setting Shield Level to largest size in array... Array Value: " + shields.Length);
            currentShieldLevel = shields.Length;
        }

        Instantiate(shields[currentShieldLevel - 1], playerObject.transform.position, Quaternion.Euler(90, 0, 0), playerObject.transform);
    }

    private void shieldPowerUp()
    {
        Debug.Log("Shield Updating 123");
    }

    public int getCurrentShieldLevel()
    {
        return currentShieldLevel;
    }

    public void setCurrentShieldLevel(int newCurrentShieldLevel)
    {
        currentShieldLevel = newCurrentShieldLevel;
    }

    public void incrementCurrentShieldLevel(int incrementValue)
    {
        currentShieldLevel += incrementValue;
    }
}
