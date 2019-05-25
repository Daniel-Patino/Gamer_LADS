using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightShotBehaviour : MonoBehaviour
{
    private Rigidbody rb;

    public float bulletSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.up * bulletSpeed;
    }
}
