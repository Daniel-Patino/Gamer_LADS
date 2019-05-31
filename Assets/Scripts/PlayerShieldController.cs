using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShieldController : MonoBehaviour
{
    public GameObject playerObject;
    public int initialShieldLevel = 0;
    public GameObject[] shields;

    private bool shieldActive = false;

    [HideInInspector] private int currentShieldLevel = 0;

    public void shieldUpdate()
    {
        if(currentShieldLevel > shields.Length)
        {
            currentShieldLevel = shields.Length;
        }
        if(currentShieldLevel <= 0) {
            return;
        }
        Debug.Log("Value of Shield: " + currentShieldLevel);
        Debug.Log("Shields");
        Instantiate(shields[currentShieldLevel - 1], playerObject.transform.position, Quaternion.Euler(90, 0, 0), playerObject.transform);
    }

    public bool isShieldActive()
    {
        return shieldActive;
    }

    public void setShieldActive(bool isShieldActive)
    {
        shieldActive = isShieldActive;
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

    public void decrementCurrentShieldLevel(int decrementValue)
    {
        currentShieldLevel -= decrementValue;
    }
}
