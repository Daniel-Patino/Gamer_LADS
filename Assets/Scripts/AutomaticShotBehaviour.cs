using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticShotBehaviour : MonoBehaviour
{
    public GameObject projectile;
    public GameObject spawnLocation;
    public float waitBeforeFirstShot = 1f;
    public float repeatShotTime = 1f;

    void Start()
    {
        InvokeRepeating("InstantiateProjectile", waitBeforeFirstShot, repeatShotTime);
    }

    private void InstantiateProjectile()
    {
        Instantiate(projectile, spawnLocation.transform.position, spawnLocation.transform.rotation);
    }
}
