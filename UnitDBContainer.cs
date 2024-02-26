using System.Collections.Generic;
using DefaultNamespace.MyScripts.UtilClasses;
using NaughtyAttributes;
using UnityEngine;

namespace DefaultNamespace.MyScripts
{
    public class UnitDBContainer : Singleton<UnitDBContainer>
    {
        [SerializeField]
        private List<UnitSO> UnitSOList = new();
        
        
        public UnitSO GetUnitSOByName(string name)
        {
            return UnitSOList.Find(x => x.name == name);
        }
        
        
        [Button][UnityEditor.Callbacks.DidReloadScripts]
        public void RefreshUnitDB()
        {
            UnitSOList.Clear();
            var unitSos = Resources.LoadAll<UnitSO>("UnitSO/");
            foreach (var unitSo in unitSos)
            {
                UnitSOList.Add(unitSo);
            }
        }
    }
}