using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Tools
{

    [MenuItem("GameObject/TD-CUSTOM/Waypoint")]
    public static void CreateWayPoint() 
    {
        GameObject go = Selection.activeGameObject;
        Transform parent = go?.transform;

        GameObject gameObject = Resources.Load("Prefabs/Waypoint/WayPoint") as GameObject;
        GameObject.Instantiate(gameObject, parent);
    }
}
