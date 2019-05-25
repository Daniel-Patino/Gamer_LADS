using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
/*
 * This class will handle various properties of the player and his inputs
 * This class will allow the player too:
 * Move his ship
 * Shoot projectiles
 * Use Powerups
 * Pause, activate the game
 * etc...
 */

public class Boundary
{
    public float xMin, xMax, yMin, yMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    public GameObject projectile;
    public GameObject bomb;
    public Transform shotSpawn;
    public bool isClamped = false;
    public Boundary boundary;

    private UserInterfaceController gameUI;
    private GameObject projectileClone;
    private Vector3 mousePosition = new Vector3();
    private Rigidbody thisRigidBody;
    private Vector3 direction;

    /*
     * This method will allow the player to move his ship by positioning his mouse anywhere on the screen
     * The ship will attempt to follow the mouse in accordance to the moveSpeed of the ship
     */ 
    private void followPlayerMouse()
    {
        Vector3 offsetMousePosition = new Vector3();
        mousePosition = Input.mousePosition; // Find the MousePosition on the x,y plane of the screen
        // The z-axis of mouseposition will be 0, we must stay in a position relative to our camera so we set the z to the offset of the camera. 
        offsetMousePosition = new Vector3(mousePosition.x, mousePosition.y, Camera.main.transform.position.y);
        mousePosition = Camera.main.ScreenToWorldPoint(offsetMousePosition);   // Convert that position to a point on the game World
    }

    /*
     * This method stops the player from moving outside of a predetermined boundary using the Boundary class
     */ 
    private void clampPlayer()
    {
        if (isClamped) {
            float xClamp = Mathf.Clamp(mousePosition.x, boundary.xMin, boundary.xMax);
            float yClamp = Mathf.Clamp(mousePosition.y, boundary.yMin, boundary.yMax);
            float zClamp = Mathf.Clamp(mousePosition.z, boundary.zMin, boundary.zMax);
            mousePosition = new Vector3(xClamp, yClamp, zClamp);
        }
    }

    /*
     * Moves the player towards the current mouse position at the speed of moveSpeed * the change in time
     */
    private void movePlayer()
    {
        thisRigidBody.transform.position = Vector3.MoveTowards(thisRigidBody.transform.position, mousePosition, moveSpeed * Time.deltaTime);

        //if (mousePosition.ToString() == thisRigidBody.transform.position.ToString())
        //{
        //    thisRigidBody.velocity = Vector3.zero;
        //}
        //else
        //{
        //    direction = (mousePosition - transform.position).normalized;
        //    thisRigidBody.velocity = new Vector3(direction.x * moveSpeed, 0.0f, direction.z * moveSpeed);
        //}
    }

    /*
     * Fires a projectile when the player presses the mouse button
     */ 
    private void fireProjectileOnButtonDown()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(projectile, shotSpawn.position, shotSpawn.rotation);
        }
    }

    private void fireBombOnButtonDown()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            if (gameUI.launchBomb())
            {
                Instantiate(bomb, shotSpawn.position, shotSpawn.rotation);
            }
        }
    }

    private void Awake()
    {
        this.gameObject.name = "Player";
        thisRigidBody = this.gameObject.GetComponent<Rigidbody>();
        gameUI = GameObject.Find("GameController").GetComponent<UserInterfaceController>();
    }

    // When dealing with Rigidbody or other Physics related scenarios we use FixedUpdate
    void FixedUpdate()
    {
        followPlayerMouse();
        clampPlayer();
        movePlayer();
    }

    void Update()
    {
        fireProjectileOnButtonDown();
        fireBombOnButtonDown();
    }
}

