using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(MeshCombiner))]
public class MeshCombinerEditor : Editor
{
    private void OnSceneGUI()
    {
        MeshCombiner meshCombiner = target as MeshCombiner;
        if (Handles.Button(meshCombiner.transform.position + Vector3.up * 5, Quaternion.LookRotation(Vector3.up), 1, 1, Handles.CylinderHandleCap))
        {
            meshCombiner.CombineMeshes();
        }
    }
}
