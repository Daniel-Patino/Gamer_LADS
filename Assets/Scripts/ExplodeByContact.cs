using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeByContact : MonoBehaviour
{
    public GameObject explosion;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Boundary")
        {
            if (other.tag != this.tag)
            {
                Instantiate(explosion, this.transform.position, this.transform.rotation);
                Destroy(this.gameObject);
            }
        }
    }
}
