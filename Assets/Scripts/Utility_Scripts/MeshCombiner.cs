using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCombiner : MonoBehaviour
{
    public void CombineMeshes()
    {
        MeshFilter[] meshesToCombine = GetComponentsInChildren<MeshFilter>();
        Mesh finalMesh = new Mesh();

        CombineInstance[] combiners = new CombineInstance[meshesToCombine.Length];

        for(int i = 0; i < meshesToCombine.Length; i++)
        {
            if (meshesToCombine[i].transform != transform)
            {
                combiners[i].subMeshIndex = 0;
                combiners[i].mesh = meshesToCombine[i].sharedMesh;
                combiners[i].transform = meshesToCombine[i].transform.localToWorldMatrix;
            }
        }

        finalMesh.CombineMeshes(combiners);

        GetComponent<MeshFilter>().sharedMesh = finalMesh;

        Debug.Log("Name: " + name + " Meshses: " + meshesToCombine.Length);
    }
}
