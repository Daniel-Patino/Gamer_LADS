using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script should handle the health of individual units this script will ensure the object
 * to which this script is attached will be destroyed when it reaches 0 health
 */

public class IndividualHealth : MonoBehaviour
{
    public int startingHealth = 1;
    [HideInInspector] public int currentHealth;
    public bool invulnerabilityOnDamage = false;
    public float invulnerabilityTime = 1f;
    public float invulnerabilityFlash = 0.5f;
    public bool canRespawn = false;

    private bool isInvulnerable = false;

    /*
     * Other objects can use this method to apply damage to this instance of IndividualHealth
     */
    public void takeDamage(int amount)
    {
        if (!isInvulnerable)
        {
            currentHealth -= amount;
            if (invulnerabilityOnDamage && currentHealth > 0) 
            {
                enterInvulnerabilityState();
            }
        }
    }

    /*
     * Call to check the current health of the object, we can elaborate this method to handle
     * many scenarios and specific tags
     */ 
    private bool checkHealth()
    {
        bool isDead = false;
        if(currentHealth > 0)
        { isDead = false; }
        else
        { isDead = true; }
        return isDead;
    }

    /*
     * 
     */
    private void enterInvulnerabilityState()
    {
        StartCoroutine(invulnerabilityState());
    }

    /*
     * The invulnerability state consists of flashing material of the Mesh Renderer by setting
     * its color alpha to 0 and then back to 1
     */ 
    IEnumerator invulnerabilityState()
    {
        MeshRenderer thisMeshRenderer = this.gameObject.GetComponentInChildren<MeshRenderer>();

        Color regularColor = new Color(1f, 1f, 1f, 1f);
        Color medAlpha = new Color(1f, 1f, 1f, 0.66f);
        Color lowAlpha = new Color(1f, 1f, 1f, 0.15f);

        isInvulnerable = true;

        for(int i = 0; i < invulnerabilityTime; i++)
        {
            thisMeshRenderer.material.color = lowAlpha;
            yield return new WaitForSeconds(invulnerabilityFlash);
            thisMeshRenderer.material.color = medAlpha;
            yield return new WaitForSeconds(invulnerabilityFlash);
        }

        thisMeshRenderer.material.color = regularColor;
        isInvulnerable = false;
    }

    /*
     * If the object is dead, we should play VFX, SFX and destroy it here
     */
    private void killObject(bool isDead)
    {
        if (isDead && !canRespawn)
        {
            Destroy(this.gameObject);
        }
        else if (isDead && canRespawn)
        {
            Destroy(this.gameObject);
        }
    }

    private void Awake()
    {
        currentHealth = startingHealth;
    }

    private void LateUpdate()
    {
        killObject(checkHealth());        
    }
 
    public int getCurrentHealth()
    {
        return currentHealth;
    }
 
    public void setCurrentHealth(int amount)
    {
        currentHealth = amount;
    }
}
