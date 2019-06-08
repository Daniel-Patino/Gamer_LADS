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
        waveReader.Awake();
        waveReader.stuffThatWorks2();


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

public class WaveReader
{
    // primary source
    private GameObject waveContainer;
    // intermediate source
    public Component[] enemyCountCarrier;
    // this is (also) a constructor. note there's no type (e.g. void).
    // when public and same name as class, allows passing of inputs.   
    public WaveReader(GameObject waveContainer)
    {
        this.waveContainer = waveContainer;
    }

    // main
    public int wavesTotal; // number of waves in this scene
    public int[] enemiesEachWave; // array, enemies on each given wave
    
    public void Awake()
    {
        // get number of waves
        wavesTotal = waveContainer.transform.childCount;

        // get number of enemies on each wave
        enemyCountCarrier = waveContainer.GetComponentsInChildren(typeof(EnemyCount), true);
        enemiesEachWave = new int[enemyCountCarrier.Length];
        for (int i = 0; i < enemyCountCarrier.Length; i++)
        {
            enemiesEachWave[i] = enemyCountCarrier[i].GetComponent<EnemyCount>().enemyCount;
        }
    }

    private int currentWaypointCount;
    private Transform[] waypointSet;
    private bool activate = true;
    private int currentWave = 0;
    private int currentWaveLength;
    private int wavesLeft;

    public void stuffThatWorks2()
    {
        wavesLeft = wavesTotal;
        while (wavesLeft > 0) // currently here. <----------
                              // to do: control when next waypoint set is written. 
                              // i don't think this 'while' actually adds anything, can do just 'if'.
        {
            if (activate == true)
            {   
                currentWaveLength = waveContainer.transform.GetChild(currentWave).childCount; // find array length
                waypointSet = new Transform[currentWaveLength]; // assign array length
                for (int i = 0; i < currentWaveLength; i++)
                {
                    // assign transforms to array
                    waypointSet[i] = waveContainer.transform.GetChild(currentWave).GetChild(i).transform;
                    Debug.Log("position " + i + " filled");
                }
                currentWave++; // move reader to next position
                activate = false;
                Debug.Log("reading complete.");
            }
            wavesLeft--;
            Debug.Log("wave read.");
        }
    }

    public void test()
    {
        activate = true;
    }
}
