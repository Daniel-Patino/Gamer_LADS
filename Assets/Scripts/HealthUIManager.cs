using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * Any object with this script attached will have a HealthUI component hover behind
 * or in front of the object. The script will manage whether to display the Healthbar
 * and ensure that if the object it is a attached too has an IndividualHealth component
 * that it will match the bar to the health, if it does not have the script then it will
 * not display the bar by default.
 */ 
public class HealthUIManager : MonoBehaviour
{
    public enum positionOfHealth { front, back }; // enums allow us to make quick and simple drop-down menus in the Editor

    public bool displayAtMaxHealth = false;
    public positionOfHealth position; // This is where the drop-down in the editor refers too
    public float tweakPosition = 0.1f;
    public Canvas healthUI = null;

    private IndividualHealth thisHealth = null;
    private Vector3 healthUIOffset;
    private bool displayHealth = false;
    [HideInInspector] public Slider thisSlider = null;

    /*
     * In the start we should check if there is even a component of IndividualHealth for this object, then
     * we determine the position to place the UI and place it at that position
     */ 
    private void Start()
    {
        if (checkForThisHealth())
        {
            displayHealth = true;
        }
        determinePositionOffset();
    }

    private void determinePositionOffset()
    {
        float positionOffset = 0;
        if (position == positionOfHealth.front && displayHealth)
        {
            positionOffset = transform.position.z - (transform.lossyScale.y / 2) - tweakPosition;
            healthUISetup(positionOffset);
        }
        else if (position == positionOfHealth.back && displayHealth)
        {
            positionOffset = transform.position.z + (transform.lossyScale.y / 2) + tweakPosition;
            healthUISetup(positionOffset);
        }
    }

    /*
     * If IndividualHealth exists within this shared gameObject then we set our global IndividualHealth
     * to the one within this Gameobject and we return true. Otherwise we return false;
     */
    private bool checkForThisHealth()
    {
        if (this.GetComponent<IndividualHealth>() != null)
        {
            thisHealth = this.GetComponent<IndividualHealth>();
            return true;
        }
        return false;
    }

    /*
     * Instantiates the healthUI with the offset and sets the gameObject this script belongs too as the
     * parent so the healthUI stays in position relative to this gameBbject
     */ 
    private void placeHealthUI()
    {
        Instantiate(healthUI, healthUIOffset, Quaternion.Euler(90, 0, 0), this.gameObject.transform);
        thisSlider = this.gameObject.GetComponentInChildren<Slider>();
        thisSlider.value = thisHealth.getCurrentHealth();
    }

    /*
     * 
     */ 
    private void healthUISetup(float positionOffset)
    {
        healthUIOffset.Set(transform.position.x, transform.position.y, positionOffset);
        if (displayAtMaxHealth)
        {
            placeHealthUI();
            thisSlider.maxValue = thisHealth.startingHealth; // Some reason the slider keeps the values between application close
            thisSlider.value = thisHealth.getCurrentHealth(); // Therefore we reset it manually here
        }
    }

    /*
     * 
     */
     public void updateUIBar()
    {
        if (!displayAtMaxHealth)
        {
            displayAtMaxHealth = true;
            determinePositionOffset();
        }
        thisSlider.value = thisHealth.getCurrentHealth();
    }
}
