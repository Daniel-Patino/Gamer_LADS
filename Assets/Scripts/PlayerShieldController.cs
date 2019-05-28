using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShieldController : MonoBehaviour
{
    public GameObject playerObject;
    public int initialShieldLevel = 0;

    public void shieldUpdate()
    {
        Debug.Log("Shield Updating");
        shieldPowerUp();
    }

    private void shieldPowerUp()
    {
        Debug.Log("Shield Updating 123");
    }

    [HideInInspector] public int shieldLevel = 0;
}
