// using Bootstrap;
using UnityEngine;

namespace MyDomain.Utils
{
    
    public class SingletonScriptableObject<T> : ScriptableObject where T : SingletonScriptableObject<T>
    {
        private static T instance;

        public static T I
        {
            get
            {
                if (instance == null)
                {
                    // // 씬에 없니?
                    // if (ServiceContainer.HasInstanceInScene() == true)
                    // {
                    //     instance =ServiceContainer.I.GetByType<T>();
                    //     return instance;
                    // }
                    T[] asset = Resources.LoadAll<T>("SingletonSO/");
                    if (asset == null || asset.Length < 1)
                    {
                        throw new System.Exception($"Put this into SingletonSO/ folder");
                    }
                    else if (asset.Length > 1)
                    {
                        Debug.Log($"<color=red>Multiple instance</color>");
                    }

                    instance = asset[0];
                }

                return instance;
            }
        }
        
    }
}