using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavePatternController : MonoBehaviour
{
    public Transform[] waypointSet;
    public GameObject Wave;

    public UserInterfaceController userInterfaceController;
    private int currentScore;
    public MeteorSpawner meteorSpawner;
    private int meteorRemainCount;

    public float startWait = 5f;

    bool initialGrace = true;

    void Awake()
    {
        // move this later
        waypointSet = new Transform[Wave.transform.childCount];
        for (int i = 0; i < waypointSet.Length; i++)
        {
            waypointSet[i] = Wave.transform.GetChild(i);
        }

        // set up first wave stuff

        initialGrace = true;
        StartCoroutine(WaveControlMainMethod());
        
    }

    /* initial timer disable everything
     * when score exceed some value, spawn next wave.
     * spawn enemies at certain time interval until wave count runs out
     * 
     * would be interesting if we could spawn multiple waves at once...
     * 
     * 
     * 
     */

    void Update()
    {
        float timeElapsed = Time.timeSinceLevelLoad;        
    }

    private float interimTimer;
    

    void WaveControlMainMethod2()
    {
        // initial freedom from enemies
        // this implementation is called a "flag".
        while (initialGrace == true)
        {
            if (Time.timeSinceLevelLoad >= 5.0)
            {
                initialGrace = false;
            }
        }





    }
    
    IEnumerator WaveControlMainMethod()
    {
        yield return new WaitForSeconds(startWait);
        for (int i = 0; i < 5; i++) // while waves AND meteors have not been exhausted
        {
            if (currentScore == 2) // if score > some threshold
            {
                // begin spawning wave
            }

        }
    }

    IEnumerator Placeholder()
    {
        yield return new WaitForSeconds(0);
    }
}