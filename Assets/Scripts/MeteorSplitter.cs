using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The object this script is attached to will spawn objects intended to act as debris when it is destroyed by projectiles
 * It will not spawn objects if it detects a collision with the playerShip and it will ignore all other except player tags.
 */ 
public class MeteorSplitter : MonoBehaviour
{
    public GameObject[] nextMeteor;
    public int objectsToSpawn = 2;

    private bool toSplit = false;
    private bool isQuitting = false;
    private bool isShield = false;

    private void Awake()
    {
        isQuitting = false;
    }

    public void collideWithShield()
    {
        isShield = true;
    }

    /*
     * When the application quits all gameObjects are destroyed, this conflicts with our
     * implementation of OnDestroy, therefore we must set a flag that will prevent meteors
     * from spawningObjects when they are destroyed by the application quitting.
     */
    private void OnApplicationQuit()
    {
        isQuitting = true;
    }

    /*
    private void ExitScene()
    {
        if (!PauseGame.InGameState)
        {
            Debug.Log("1.2");
            isQuitting = true;
        }
    }
    */

    /*
     * 
     */
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") // <= check if player is the ship itself or the projectile
        {
            if (other.name == "Player") // Contact with ship
            {
                toSplit = false;
            }
            else // Contact with bullets or other
            {
                toSplit = true;
            }
        }
    }

    /*
     * If the object is destroyed then we check if it should be split depending on last collision detected
     */
    private void OnDestroy()
    {
        if (!PauseGame.InGameState)
        {
            isQuitting = true;
        }

        if (toSplit && !isQuitting && !isShield)
            spawnObjects();
    }

    /*
     * 
     */
    private void spawnObjects()
    {
        Vector3 insideSphere = new Vector3();
        for (int i = 0; i < objectsToSpawn; i++)
        {
            insideSphere = ((Random.insideUnitSphere * 0.5f) + transform.position); // Find a position inside of sphere at position of the transform
            insideSphere.Set(insideSphere.x, 0.0f, insideSphere.z);
            Instantiate(nextMeteor[Random.Range(0, nextMeteor.Length)], insideSphere, Quaternion.Euler(90, 0, Random.Range(160, 200)));
            
        }
    }
}
