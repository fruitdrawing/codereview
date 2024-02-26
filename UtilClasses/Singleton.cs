using UnityEngine;

namespace DefaultNamespace.MyScripts.UtilClasses
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;
        public static T I
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    // if (Application.isPlaying == false) return null;

                    if (instance == null)
                    {
                        Debug.LogError($"<color=red>[Singleton] New GameObject Created : {typeof(T)}</color>");
                        instance = new GameObject().AddComponent<T>();
                    }
                }
                return instance;
            }
        }
       
        
    }
}