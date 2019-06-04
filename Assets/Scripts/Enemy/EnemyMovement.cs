using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 10f;
    public WavePatternController wavePatternController;

    private Transform currentWaypoint;
    private int WaypointIndex = 0;
    
    void Awake()
    {
        currentWaypoint = wavePatternController.waypointSet[0];
    }
    
    void Update()
    {
        Vector3 dir = currentWaypoint.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, currentWaypoint.position) <= 0.2f)
        {
            NextWaypoint();
        }
    }

    void NextWaypoint()
    {
        if (WaypointIndex >= wavePatternController.waypointSet.Length - 1)
        {
            Destroy(gameObject);
            return;
        }

        WaypointIndex++;
        currentWaypoint = wavePatternController.waypointSet[WaypointIndex];
    }
}
