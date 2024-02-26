using DefaultNamespace.MyScripts.TimeRelated;
using DefaultNamespace.MyScripts.UtilClasses;
using NaughtyAttributes;
using UnityEngine;

namespace DefaultNamespace.MyScripts.MyTestRelated
{
    public class MyTestManager : Singleton<MyTestManager>
    {
        [Button]
        public void TESTTT()
        {
            Debug.Log($"<color=white>{ TimeManager.I.CurrentTimeScale.Value}</color>");
           
        }
    }
}