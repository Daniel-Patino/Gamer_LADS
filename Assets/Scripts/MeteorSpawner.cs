using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{

    public GameObject[] meteorHazard;
    public Vector3 spawnArea;
    public int spawnCount;
    public float spawnWait;
    public float startWait;
    public int spawnAngleOffset = 0;

    [HideInInspector] public int spawnCountRemaining;

    // Start is called before the first frame update
    void Start()
    {
        spawnCountRemaining = spawnCount;
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);

        // desire to control number of meteors on screen.
        /*
         * searching for total number of instances in-scene is probably prohibitive. so can't do true count.
         * currently there's 4 meteors that spawn. all of them are "big", i.e. 3 shots to split. but they're all different sizes... feels like this is really bad from a recognition standpoint. i couldn't figure it out at least.
         * currently also: setup seems to have a constant amount of points per stage with any given setting. spawner only spawns bigs, bigs always spawn 2 meds, meds always spawn 2 smalls. which is good. and since speed is constant that means number of points on screen is also constant...
         * 
         * is it just a balance problem? of how fast to spawn things / how many hits / etc.?
         * 
         */
        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnArea.x, spawnArea.x), spawnArea.y, spawnArea.z);
            Quaternion spawnRotation = Quaternion.Euler(90, 0, Random.Range(180, 180));
            Instantiate(meteorHazard[Random.Range(0, meteorHazard.Length)], spawnPosition, spawnRotation);

            spawnCountRemaining--;

            yield return new WaitForSeconds(spawnWait);
        }
    }
}
