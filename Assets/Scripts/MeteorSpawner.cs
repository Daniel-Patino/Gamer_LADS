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
