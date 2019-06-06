using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavePatternController : MonoBehaviour
{
    // Unsorted Active
    public GameObject WaveContainer;
    private WaveReader waveReader;

    // Unsorted Leftovers

    // Readers
    public UserInterfaceController userInterfaceController;
    private int currentScore;
    public MeteorSpawner meteorSpawner;
    private int meteorRemainCount;

    // Balance Variables
    public float startWait = 5f;

    // Internal Use
    bool initialGrace = true;
    public Transform[] waypointSet;

    void Awake()
    {
        //--- TEMP TEST ---
        // waypoint objects to array
        // waypointSet = new Transform[Wave.transform.childCount];
        // for (int i = 0; i < waypointSet.Length; i++)
        // {
        //     waypointSet[i] = Wave.transform.GetChild(i);
        // }

        // reader/writer of waves
        waveReader = new WaveReader(WaveContainer);
        waveReader.test();


        //--- TEMP TEST ---

        // set up first wave stuff

        initialGrace = true;
        
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
    

    void WaveControlMainMethod()
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

        // wouldnt it be interesting if you could have two waves at once
        // i guess i should make one work first

        // while (meteorRemainCount > 0)
        // {
        //     while (waveRemainCount > 0)
        //     {
        //         if (scoreInterval > 100 && currentEnemyCount = 0)
        //         {
        //             // begin wave spawn
        //             // scoreInterval = 0
        //         }
        //     }
        // }

    }
    
    IEnumerator Placeholder()
    {
        yield return new WaitForSeconds(0);
    }

}

// why cant i call this private
public class WaveReader
{
    public int wavesTotal;
    public Component[] enemyCountCarrier;
    public int[] enemiesEachWave;
    private GameObject waveContainer;

    // this is a constructor. note there's no type (e.g. void).
    // when public and same name as class, allows passing of inputs.   
    public WaveReader(GameObject waveContainer)
    {
        this.waveContainer = waveContainer;
    }

    public void test()
    {
        // get number of waves
        wavesTotal = waveContainer.transform.childCount;

        // get number of enemies on each wave
        enemyCountCarrier = waveContainer.GetComponentsInChildren(typeof(EnemyCount), true);
        enemiesEachWave = new int[enemyCountCarrier.Length];
        for (int i = 0; i<enemyCountCarrier.Length; i++)
        {
            // Debug.Log(enemyCountCarrier[i]);
            enemiesEachWave[i] = enemyCountCarrier[i].GetComponent<EnemyCount>().enemyCount;
            Debug.Log(enemiesEachWave[i]);
        }
            




        // get transforms for each wave's points
    }
}
