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

public class WaveReader
{
    // source
    private GameObject waveContainer;
    // number of waves in this scene
    public int wavesTotal;
    // array, enemies on each given wave
    public Component[] enemyCountCarrier;
    public int[] enemiesEachWave;
    
    // this is (also) a constructor. note there's no type (e.g. void).
    // when public and same name as class, allows passing of inputs.   
    public WaveReader(GameObject waveContainer)
    {
        this.waveContainer = waveContainer;
    }
    
    public void stuffThatWorks()
    {
        // get number of waves
        wavesTotal = waveContainer.transform.childCount;

        // get number of enemies on each wave
        enemyCountCarrier = waveContainer.GetComponentsInChildren(typeof(EnemyCount), true);
        enemiesEachWave = new int[enemyCountCarrier.Length];
        for (int i = 0; i < enemyCountCarrier.Length; i++)
        {
            enemiesEachWave[i] = enemyCountCarrier[i].GetComponent<EnemyCount>().enemyCount;
            // Debug.Log(enemiesEachWave[i]);
        }
    }

    private Transform[] testRead;
    private Transform[] testR2;
    private Transform[] testR3;
    private int currentWaypointCount;
    private Transform[] waypointSet;
    private bool activate = true;
    private int currentWave = 0;
    private int currentWaveLength;

    public void test()
    {
        // get transforms for each wave's points

        /*
            * get children of child [i]
            * - count how many
            * - getcomponent transform, stick them in a Vector3 array
            */

        // can't stick constructor inside a loop
        // waypointSet = new Transform[waveContainer.transform.GetChild(0).childCount];

        /*
        * array: lengths of waves
        * loop: assign new length of array, then assign transforms to each position
        * 
        * this could maybe be simplified with a matrix, but i don't want to do matrices.
        */

        waypointSet = new Transform[currentWaveLength]; // <--- this is where i left off

        //untested
        if(activate == true)
        {
            currentWaveLength = waveContainer.transform.GetChild(currentWave).childCount;
            // Debug.Log(currentWaveLength);
            // waypointSet = 
            for (int i = 0; i < currentWaveLength; i++)
            {
                // waypointSet[i] = waveContainer.transform.GetChild(currentWave).GetChild(i).transform;
                Debug.Log("position " + i + " filled");
            }
            currentWave++;
            activate = false;
        }
        
        // constructor must occur inside of a method
        // --- this stuff works ---
        // testRead = new Transform[2];
        // testRead[0] = waveContainer.transform.GetChild(0).GetChild(0).transform;
        // testRead[1] = waveContainer.transform.GetChild(1).GetChild(1).transform;
        // if (testRead[1] != null)
        // {
        //     Debug.Log("apparently transform has been read.");
        // }
    }
}
