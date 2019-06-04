using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * All objects with this script attached will damage the other objects, for now
 * we will only do damage once per collision
 * 
 * Objects with this script should not be able to damage objects with the same tag.
 */
public class DamageByContact : MonoBehaviour
{
    public int damage = 1;

    private IndividualHealth otherHealth = null;
    private HealthUIManager otherUI = null;

    /*
     * When we enter a collision box of another object we should check what object it is
     * so we may grab the "IndividualHealth" component of that other object.
     */ 
    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("This: " + this.name + " enters " + other.name);

        // "this" is giver of damage, "other" is receiver.
        // 4 types, 16 combinations, see excel table for visualization.
        if (other.tag != this.tag)
        {

            if (other.tag == "Player")
            {
                otherHealth = other.GetComponent<IndividualHealth>();
                otherUI = other.GetComponent<HealthUIManager>();
                damageOther();
            }

            else if (other.tag == "Obstacle")
            {
                if (this.tag != "Enemy")
                {
                    otherHealth = other.GetComponent<IndividualHealth>();
                    otherUI = other.GetComponent<HealthUIManager>();
                    damageOther();
                }
            }

            else if (other.tag == "Enemy")
            {
                if (this.tag == "Player")
                {
                    otherHealth = other.GetComponent<IndividualHealth>();
                    otherUI = other.GetComponent<HealthUIManager>();
                    damageOther();
                }
            }

            else if (other.tag == "EnemyProjectile")
            {
                if (this.tag == "Obstacle")
                {
                    otherHealth = other.GetComponent<IndividualHealth>();
                    otherUI = other.GetComponent<HealthUIManager>();
                    damageOther();
                }
            }

            else if (other.tag == "Boundary")
            {

            }

            else {
                Debug.Log("Damage system encountered something unexpected.");
            }

            // is there a way to make this shorter?
            // i can't stick it in damageOther because it doesn't know what "other" is.

            //otherHealth = other.GetComponent<IndividualHealth>();
            //otherUI = other.GetComponent<HealthUIManager>();
            //damageOther();
        }
    }
    

    /*
     * When we exit a collision we want to relinquish the IndividualHealth of the other object
     * and essentually reset this component for the next encoounter.
     */
    private void OnTriggerExit(Collider other)
    {
        otherHealth = null;
    }

    /*
     * Access the IndividualHealth if it exists and is not null and access the class to
     * remove health from the other object.
     */ 
    private void damageOther()
    {
        if(otherHealth != null)
        {
            otherHealth.takeDamage(damage);
        }
        if(otherUI != null)
        {
            otherUI.updateUIBar();
        }
    }
}
