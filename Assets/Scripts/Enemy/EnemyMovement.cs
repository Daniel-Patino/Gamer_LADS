using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f;
    private WavePatternController wavePatternController;

    private bool signalSent = false;

    private Transform[] waypointSet;
    private Transform currentWaypoint;
    private int WaypointIndex = 0;

    private void Awake()
    {
        // this exists because prefabs don't accept nonstatic classes.
        // information of that class must be passed for this class to work.
        // this is a workaround.
        wavePatternController = GameObject.Find("EnemyController").GetComponent<WavePatternController>();
    }

    void Start()
    {
        waypointSet = wavePatternController.getWaypointSet();
        currentWaypoint = waypointSet[0];
    }

    /* private bool print0 = false;
    * private bool print1 = false;
    * private bool print2 = false;
    */
    void Update()
    {
        Vector3 dir = currentWaypoint.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, currentWaypoint.position) <= 0.2f)
        {
            NextWaypoint();
        }

        /* if (WaypointIndex == 0 && print0 == false)
        * {
        *     Debug.Log(WaypointIndex);
        *     print0 = true;
        * }
        * else if (WaypointIndex == 1 && print1 == false)
        * {
        *     Debug.Log(WaypointIndex);
        *     print1 = true;
        * }
        * else if (WaypointIndex == 2 && print2 == false)
        * {
        *     Debug.Log(WaypointIndex);
        *     print2 = true;
        * }
        */ 
            
        if (WaypointIndex == 2 && signalSent == false)
        {
            // some sort of signal... what would it be?
            wavePatternController.movementTriggeredSignal = true;
            signalSent = true;
            Debug.Log(signalSent);
        }
    }

    void NextWaypoint()
    {
        if (WaypointIndex >= waypointSet.Length - 1)
        {
            Destroy(gameObject);
            return;
        }
        
        WaypointIndex++;
        currentWaypoint = waypointSet[WaypointIndex];
    }
}
