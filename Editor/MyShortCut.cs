using System.Linq;
using SharedData;
using UnityEditor;
using UnityEngine;

public class MyShortCut : MonoBehaviour
{
    [MenuItem("MyShortCut/LoadResources %l")]
    public static void LoadResourcesAll()
    {
        var unityObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IHasResourcesToLoad>().ToArray();;
        foreach (var VARIABLE in unityObjects)
        {
            VARIABLE.LoadResources();
            Debug.Log($"<color=white>{VARIABLE.GetType().Name} has loaded resources.</color>");
        }
        
    }
   
}
