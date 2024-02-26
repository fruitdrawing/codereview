using System.Collections.Generic;
using DefaultNamespace.MyScripts.SharedData;
using DefaultNamespace.MyScripts.UtilClasses;
using Pathfinding;
using Redcode.Pools;
using SharedData;
using UniRx;
using UnityEngine;

namespace DefaultNamespace.MyScripts
{
    public class SpawnUnitManager : Singleton<SpawnUnitManager>
    {
        public GameObject Unit_Prefab;
        public Dictionary<int, GameObject> SpawnedDictionary = new();
        public PoolManager UnitPool;
        private void Awake()
        {
            
        }

        


        public bool TrySpawnTo(UnitSpawnData unitSpawnData,Vector3 targetPosition)
        {
            if (unitSpawnData.UnitSO == null)
            {
                Debug.LogWarning("UnitSO is null");
                return false;
            }
            var pool = UnitPool.GetPool<Transform>();
            
            var spawned = Instantiate(Unit_Prefab, targetPosition, Quaternion.identity);
            spawned.name = $"{unitSpawnData.OwnerName}";
            
            var characterCore = spawned.GetComponent<CharacterCore>().InitByUnitSpawnData(unitSpawnData);
            characterCore.DeathBehaviour.OnDeath.AsObservable().Subscribe(_ =>
            {
                if (_.CurrentTeam.Value == UnitTeam.EnemyTeam)
                {
                    GameManager.I.AddMoney(100,UnitTeam.PlayerTeam);
                }
                else
                {
                    GameManager.I.AddMoney(100,UnitTeam.EnemyTeam);
                }
            }).AddTo(this);
            
            MessageBroker.Default.Publish(unitSpawnData);
            SpawnedDictionary.Add(spawned.GetInstanceID(),spawned);
            return true;
            
        }
        
        public void DespawnUnitByInstanceId(GameObject gameObject)
        {
            int instanceId = gameObject.GetInstanceID();
            
            if (SpawnedDictionary.TryGetValue(instanceId, out var found))
            {
                Destroy(found);
                SpawnedDictionary.Remove(instanceId);
            }
        } 
        
        
      
        
        
    }
}