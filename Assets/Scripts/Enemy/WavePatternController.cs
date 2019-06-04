using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavePatternController : MonoBehaviour
{
    public Transform[] waypointSet;
    public GameObject Wave;
    public int test = 0;

    void Start()
    {

        waypointSet = new Transform[Wave.transform.childCount];
        for (int i = 0; i < waypointSet.Length; i++)
        {
            waypointSet[i] = Wave.transform.GetChild(i);
        }
    }
}