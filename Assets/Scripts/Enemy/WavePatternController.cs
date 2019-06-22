using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavePatternController : MonoBehaviour
{
    // Unsorted Active
    public GameObject WaveContainer;
    private GameObject WavePattern;
    private WaveReader waveReader;
    public GameObject Enemy;

    // Readers
    public UserInterfaceController userInterfaceController;
    private int currentScore;
    private int prevWaveSpawnScore;

    public MeteorSpawner meteorSpawner;
    private int meteorRemainCount;

    // Carriers
    [HideInInspector] public Transform[] waypointSet;

    // Balance Variables
    public float startWait = 5f;
    public float interimWait = 5f;
    public float waveWait = 5f;
    public int scoreIntToSpawnNextWave = 10;

    // Internal Use
    private bool initialGrace = true;
    private float timeSincePreviousWaveBegan = 0;
    private bool waveActive = false;

    void Awake()
    {
        // connecting the classes
        waveReader = new WaveReader(WaveContainer);
        waveReader.InitialSetup();

        // reinitialize
        initialGrace = true;
        timeSincePreviousWaveBegan = 0;
        prevWaveSpawnScore = 0;
        waveActive = false;
            // reset waves to initial state: only first child active
        if (WaveContainer.transform.childCount != 0)
        {
            for (int i = 0; i < waveReader.wavesTotal; i++)
            {
                WaveContainer.transform.GetChild(i).gameObject.SetActive(false);
            }
            WaveContainer.transform.GetChild(0).gameObject.SetActive(true);
        }

        // TEST AREA
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
        // most of the stuff that controls spawn conditions (ienumerator has the last one)
        float timeElapsed = Time.timeSinceLevelLoad;
        meteorRemainCount = meteorSpawner.spawnCountRemaining;
        currentScore = userInterfaceController.waveTextHandler.getCurrentScore();
        int scoreInterval = currentScore - prevWaveSpawnScore;
        float timeInterval = timeElapsed - timeSincePreviousWaveBegan;

        // initial grace period
        if (initialGrace == true && timeElapsed > startWait)
        {
            initialGrace = false;
                Debug.Log("initial grace ended");
        }

        // main executor
        // statements ordered descending most likely to fail
        if (initialGrace == false)
        {
            if (scoreInterval > scoreIntToSpawnNextWave)
            {
                // Debug.Log("meteor: " + meteorRemainCount + " | wavesleft: " + waveReader.wavesLeft + " | waveactive: " + waveActive + " | timeinterval: " + timeInterval);
                if (meteorRemainCount > 0
                    && waveReader.wavesLeft > 0
                    && waveActive == false
                    && timeInterval > waveWait)
                {
                    timeSincePreviousWaveBegan = timeElapsed; // reset time interval
                    prevWaveSpawnScore = currentScore; // reset score interval

                    waveReader.WavePointSetter();
                        Debug.Log("waypoint setter activated");
                    StartCoroutine(NewWaveSpawner());
                        Debug.Log("wave spawner activated");
                    waveReader.setNextWave();
                        Debug.Log("pointer moved to next set");

                    // ---> spawns 5 enemies first wave, red error second wave.
                    /* wavereader.initialsetup writes the correct enemy count into enemieseachwave...
                     * 
                     * problem is that waveReader.setNextWave() occurs before NewWaveSpawner.EnemiesToSpawn.
                     * i.e. the information isn't read into NewWaveSpawner before it switches...
                     */
                }

            }
        }

    }

    //
    IEnumerator NewWaveSpawner()
    {
        waveActive = true;

        Debug.Log("spawn buffer wait");
        yield return new WaitForSeconds(waveWait); // initial wait
        int EnemiesToSpawn = waveReader.getWaveEnemyCount(waveReader.getCurrentWave(), waveReader.enemiesEachWave); // this long thing is just a bunch of getters.
        float timing = interimWait;

        Debug.Log(EnemiesToSpawn);

        for (int i = 0; i < EnemiesToSpawn; i++)
        {
            Vector3 spawnPosition = new Vector3(0, 0, 10.5f);
            Quaternion spawnRotation = Quaternion.Euler(90, 0, 0);
            Instantiate(Enemy, spawnPosition, spawnRotation);

            Debug.Log("spawn enemy");
            if (i == EnemiesToSpawn - 1)
            {
                Debug.Log("end coroutine");
                // yield return new WaitForSeconds(0); // end coroutine
                break;
            }
            else
            {
                Debug.Log("wait " + timing);
                yield return new WaitForSeconds(timing); // interval wait
            }
        }

        waveActive = false;
    }

    // carrier
    public Transform[] getWaypointSet()
    {
        return waveReader.getWaypointSet();
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