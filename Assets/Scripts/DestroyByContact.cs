using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The object with this script attached must destroy itself utterly when it collides 
 * with another object, useful for projectiles and items that can only be used once and do
 * not have individual health
 */
public class DestroyByContact : MonoBehaviour
{
    public bool isPowerUp = false;
    /*
     * We must ignore the omnipresent Boundary, then we simply destroy the object itself once
     * it has collided with another object, the only exception is we must not destroy objects
     * with the same tag
     */ 
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Boundary")
        {
            if (isPowerUp)
            {
                if(other.name == "Player")
                {
                    Destroy(this.gameObject);
                }
            }

            if (other.tag != this.tag)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
