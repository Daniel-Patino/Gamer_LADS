using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualScore : MonoBehaviour
{
    public int score = 1;

    private IndividualHealth unitsHealth;
    private UserInterfaceController scoreController;

    private void Start()
    {
        scoreController = GameObject.Find("GameController").GetComponent<UserInterfaceController>();    
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (this.gameObject.GetComponent<IndividualHealth>() != null)
            {
                if (other.gameObject.GetComponent<DamageByContact>() != null)
                {
                    unitsHealth = this.gameObject.GetComponent<IndividualHealth>();
                    if (unitsHealth.getCurrentHealth() - other.gameObject.GetComponent<DamageByContact>().damage < 1)
                    { 
                        int currentScore = scoreController.waveTextHandler.getCurrentScore();
                        scoreController.waveTextHandler.addToCurrentScore(score);
                        scoreController.waveTextHandler.setScoreWaveText();
                    }
                }
            }
        }
    }
}
