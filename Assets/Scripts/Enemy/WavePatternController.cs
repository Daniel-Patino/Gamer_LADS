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

    // Readers
    public UserInterfaceController userInterfaceController;
    private int currentScore;
    private int prevWaveSpawnScore;
    public int scoreIntToSpawnNextWave;
    public MeteorSpawner meteorSpawner;
    private int meteorRemainCount;

    // Balance Variables
    public float startWait = 5f;
    public float waveWait = 5f;

    // Internal Use
    private bool initialGrace = true;
    public Transform[] waypointSet;
        // the above is currently called by EnemeyMovement.
        // it has been moved to a getter under WaveReader.
    private float timeSincePreviousWaveBegan = 0;

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

        // TEST AREA
        StartCoroutine(NewWaveSpawner());
        Debug.Log("does this line appear at 0 or 3s?"); // it appears at 0s.
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
        int scoreInterval = currentScore - prevWaveSpawnScore;
        float timeInterval = timeSincePreviousWaveBegan - timeElapsed;

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
                if (meteorRemainCount > 0
                    && waveReader.wavesLeft > 0
                    && waveSpawner.getCurrentEnemyCount() == 0
                    && timeInterval > waveWait)
                {
                    timeSincePreviousWaveBegan = timeElapsed;
                    prevWaveSpawnScore = currentScore; // reset score interval

                    // waveReader.WavePointSetter
                    // waveSpawner
                    // waveReader.setNextWave
                        /*
                         * wavespawner is now coroutine.
                         * lines after coroutine call begin after coroutine is called (not when finished).
                         * this means next-wave type things either
                         *  - need to happen inside the coroutine, at the end, or
                         *  - coroutine needs its own read-only dataset.
                         * latter sounds better.
                         */
                }

            }
        }

    }

    //
    IEnumerator NewWaveSpawner()
    {
        Debug.Log("ienumerator start.");
        yield return new WaitForSeconds(3); // initial wait
        Debug.Log("ienumerator end after designated 3 seconds");

        for (int i = 0; i < 5; i++)
        {
        
            yield return new WaitForSeconds(0); // interval wait
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

        // reset
        activate = true;
        currentWave = 0;
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
        currentWaveLength = waveContainer.transform.GetChild(currentWave).childCount; // findarraylength
        waypointSet = new Transform[currentWaveLength]; // assign array length
        for (int i = 0; i < currentWaveLength; i++)
        {   // assign transforms to array
            waypointSet[i] = waveContainer.transform.GetChild(currentWave).GetChild(i).transform;
        }
    }

    // getters and setters
    public int getCurrentWave()
    {
        return currentWave;
    }
    public int getWaveEnemyCount(int currentWave, int[] enemiesEachWave)
    {
        return enemiesEachWave[currentWave];
    }
    public Transform[] getWaypointSet()
    {
        return waypointSet;
    }
    public void setNextWave()
    {
        currentWave++;
        wavesLeft--;
        activate = false;
    }
}

public class WaveSpawner
{
    private int currentEnemyCount;

    public int getCurrentEnemyCount()
    {
        return currentEnemyCount;
    }
    // currently here <----------
    /* enemies left = current enemy count
     *      spawn enemy
     *      
     *      timeElapsedSinceLastEnemy = 0
     *      enemies--
     * 
     */

    // i think using ienumerator is probably unavoidable here...
}