using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    private void AnimEventDestroyThisObject()
    {
        Destroy(this.gameObject);
    }
}
