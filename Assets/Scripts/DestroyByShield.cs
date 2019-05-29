using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByShield : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Boundary")
        {
            if(other.tag != "Player")
            {
                Destroy(this.gameObject);
            }
        }
    }
}
