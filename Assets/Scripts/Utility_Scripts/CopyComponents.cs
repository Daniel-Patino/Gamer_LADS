using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This utility script will be used to allow to quickly copy components with or without current settings
 * quickly between gameObjects
 */ 
public class CopyComponents : MonoBehaviour
{
    public GameObject sourceObject = null;
    public MonoBehaviour sourceMono = null;

    public GameObject[] targetObject = null;

    public void copyScriptsOfComponents()
    {
        Component[] allComponentsFromSource = sourceObject.GetComponents<Component>();
        Debug.Log("All Components: " + allComponentsFromSource.ToString());

        for (int i = 0; i < targetObject.Length; i++)
        {
            Component[] allComponentsForTarget = targetObject[i].GetComponents<Component>();

            for (int j = 0; j < allComponentsForTarget.Length; j++)
            {
                System.Type typeOfComponent = allComponentsForTarget[j].GetType();
                Debug.Log("TypeOfComponent: " + typeOfComponent);
                //if (typeOfComponent == typeof(ScriptableObject);
            }
        }
    }

    /*
     * As the name implies, this method allows us to addScripts from our SourceMono to our TargetObject[]
     */ 
    public void addScripts()
    {
        MonoBehaviour[] scriptList = sourceMono.GetComponents<MonoBehaviour>();

        for(int i = 0; i < targetObject.Length; i++)
        {
            for(int j = 0; j < scriptList.Length; j++)
            {
                targetObject[i].AddComponent(scriptList[j].GetType());
                System.Reflection.FieldInfo[] fields = scriptList[j].GetType().GetFields();

                for(int k = 0; k < fields.Length; k++)
                {
                    fields[k].SetValue(targetObject[i].GetComponent(scriptList[j].GetType()), fields[k].GetValue(scriptList[j]));
                }
            }
        }
    }
}
