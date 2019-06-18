using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavePatternController : MonoBehaviour
{
    // Unsorted Active
    public GameObject WaveContainer;
    private GameObject WavePattern;
    private WaveReader waveReader;
    private WaveSpawner waveSpawner;

    // Unsorted Leftovers

    // Readers
    public UserInterfaceController userInterfaceController;
    private int currentScore;
    private int prevWaveSpawnScore;
    private int scoreInterval;
    public int scoreIntToSpawnNextWave;
    public MeteorSpawner meteorSpawner;
    private int meteorRemainCount;

    // Balance Variables
    public float startWait = 5f;

    // Internal Use
    bool initialGrace = true;
    public Transform[] waypointSet;

    void Awake()
    {
        // connecting the classes
        waveReader = new WaveReader(WaveContainer);
        waveReader.InitialSetup();
        waveSpawner = new WaveSpawner();

        // reset waves to initial state: only first child active
        if (WaveContainer.transform.childCount != 0)
        {
            for (int i = 0; i < waveReader.wavesTotal; i++)
            {
                WaveContainer.transform.GetChild(i).gameObject.SetActive(false);
            }
            WaveContainer.transform.GetChild(0).gameObject.SetActive(true);
        }

        waveReader.WavePointSetter(); // this should probably be moved somewhere else

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
        meteorRemainCount = meteorSpawner.spawnCountRemaining;

        currentScore = userInterfaceController.waveTextHandler.getCurrentScore();
        scoreInterval = currentScore - prevWaveSpawnScore;

        // initial grace period
        if (initialGrace == true && timeElapsed > startWait)
        {
            initialGrace = false;
        }

        // main executor
        // statements ordered descending most likely to fail
        if (initialGrace == false)
        {
            if (scoreInterval > scoreIntToSpawnNextWave)
            {
                if (meteorRemainCount > 0 && waveReader.wavesLeft > 0
                    && waveSpawner.getCurrentEnemyCount() == 0)
                {
                    // reset time when enemies last spawned
                    prevWaveSpawnScore = currentScore; // reset score interval

                    // waveReader
                    // waveSpawner
                }

            }
        }

    }

    // empty
    private float interimTimer;
    void WaveControlMainMethod()
    {
        // ---> apparently whiles don't work this way. or something to do with time. or something.
        // whiles aren't coroutines is maybe the way to phrase it.

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

        // while (meteorRemainCount > 0 && waveReader.wavesLeft > 0)
        // {
        //     Debug.Log("test");
        //     // if (scoreInterval > scoreIntToSpawnNextWave && currentEnemyCount = 0)
        //     // {
        //     //     // wavereader pulls waypoint transforms for current wave
        //     //     // wavespawner call
        //     //     // scoreWhenPrevWaveSpawned = currentScore
        //     // }
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
    
    // initial read. run once.
    public void InitialSetup()
    {
        // get number of waves
        wavesTotal = waveContainer.transform.childCount;
        // set counter for remaining waves
        wavesLeft = wavesTotal;
        // get number of enemies on each wave
        enemyCountCarrier = waveContainer.GetComponentsInChildren(typeof(EnemyCount), true);
        enemiesEachWave = new int[enemyCountCarrier.Length];
        for (int i = 0; i < enemyCountCarrier.Length; i++)
        {
            enemiesEachWave[i] = enemyCountCarrier[i].GetComponent<EnemyCount>().enemyCount;
        }
        // starter for WavePointSetter
        activate = true;
    }

    private int currentWaypointCount;
    private Transform[] waypointSet;
    public bool activate = true;
    private int currentWave = 0;
    private int currentWaveLength;
    public int wavesLeft;

    // writes current waypoint set to be read by spawner
    public void WavePointSetter()
    {
        while (wavesLeft > 0) // currently here (1/2) <----------
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
                }
                currentWave++; // move reader to next position
                activate = false;
            }
            wavesLeft--;
        }
    }

    public void test()
    {
        activate = true;
    }
}

public class WaveSpawner
{
    private int currentEnemyCount;

    public int getCurrentEnemyCount()
    {
        return currentEnemyCount;
    }
    // currently here (2/2) <----------
    // private enemies left
    // enemies left = current enemy count
    /* while enemies left > 0 && timeElapsedSinceLastEnemy > 5
     *      spawn enemy
     *      
     *      timeElapsedSinceLastEnemy = 0
     *      enemies--
     * 
     */
}