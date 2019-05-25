using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(CopyComponents))]
[CanEditMultipleObjects]

public class CopyComponentsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        CopyComponents components = target as CopyComponents;

        GUILayout.BeginVertical("Box");
        if(GUILayout.Button("Copy Components"))
        {
            components.copyScriptsOfComponents();
        }

        if(GUILayout.Button("Attach Scripts"))
        {
            components.addScripts();
        }
        GUILayout.EndHorizontal();
    }
}
