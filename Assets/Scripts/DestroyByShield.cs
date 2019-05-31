using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByShield : MonoBehaviour
{
    private MeteorSplitter meteorSplitter;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Boundary")
        {
            if(other.tag != "Player")
            {
                Destroy(this.gameObject);
                if(other.gameObject.GetComponent<MeteorSplitter>() != null)
                {
                    other.gameObject.GetComponent<MeteorSplitter>().collideWithShield();
                    Destroy(other.gameObject);
                }
            }
        }
    }
}
