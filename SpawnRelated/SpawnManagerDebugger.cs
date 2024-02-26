using System;
using DefaultNamespace.MyScripts.AudioRelated;
using DefaultNamespace.MyScripts.SharedData;
using DefaultNamespace.MyScripts.SkillRelated;
using DefaultNamespace.MyScripts.UtilClasses;
using NaughtyAttributes;
using SharedData;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UtilClasses;

namespace DefaultNamespace.MyScripts.SpawnRelated
{
    public class SpawnManagerDebugger : MonoBehaviour
    {
        [SerializeField]
        private SpawnUnitManager spawnUnitManager;
        
        [Button]
        public void DespawnRandomTest()
        {
            var got = spawnUnitManager.SpawnedDictionary.GetRandomFromEnumerable().Value;
            spawnUnitManager.DespawnUnitByInstanceId(got);
        }

        private void Awake()
        {
            DebugSpawnAndAttackKeyCode();
        }

        private Vector3 GetMousePosition()
        {
            
            var worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPoint = new Vector3(worldPoint.x, worldPoint.y, 0);
            return worldPoint;
            
        }
        private void DebugSpawnAndAttackKeyCode()
        {
            
            this.UpdateAsObservable().Where(_ => Input.GetKey(KeyCode.Alpha1))
                .ThrottleFirst(TimeSpan.FromSeconds(0.1f))
                .Subscribe(_ =>
                {
                    var unitSpawnData = new UnitSpawnData($"Jojo{UnityEngine.Random.Range(0, 1000)}", UnitTeam.PlayerTeam,true,UnitDBContainer.I.GetUnitSOByName("NormalAlien"));
                
                    spawnUnitManager.TrySpawnTo(unitSpawnData,GetMousePosition());
                }).AddTo(this); 

            this.UpdateAsObservable().Where(_ => Input.GetKey(KeyCode.Alpha2))
                .ThrottleFirst(TimeSpan.FromSeconds(0.1f))
                .Subscribe(_ =>
                {
                    var unitSpawnData = new UnitSpawnData($"Jojo{UnityEngine.Random.Range(0, 1000)}", UnitTeam.PlayerTeam,true,UnitDBContainer.I.GetUnitSOByName("Knight"));
                
                    spawnUnitManager.TrySpawnTo(unitSpawnData,GetMousePosition());
                }).AddTo(this); 

             this.UpdateAsObservable().Where(_ => Input.GetKey(KeyCode.Alpha3))
                .ThrottleFirst(TimeSpan.FromSeconds(0.1f))
                .Subscribe(_ =>
                {
                    var unitSpawnData = new UnitSpawnData($"Jojo{UnityEngine.Random.Range(0, 1000)}", UnitTeam.PlayerTeam,true,UnitDBContainer.I.GetUnitSOByName("OctoAlien"));
                
                    spawnUnitManager.TrySpawnTo(unitSpawnData,GetMousePosition());
                }).AddTo(this); 

            
            
            this.UpdateAsObservable().Where(_ => Input.GetKeyDown(KeyCode.S)).Subscribe(_ =>
            {
                var unitSpawnData = new UnitSpawnData($"Jojo{UnityEngine.Random.Range(0, 1000)}", UnitTeam.PlayerTeam,true,UnitDBContainer.I.GetUnitSOByName("NormalAlien"));
                
                spawnUnitManager.TrySpawnTo(unitSpawnData,GetMousePosition());
            }).AddTo(this); 
            
            
            this.UpdateAsObservable().Where(_ => Input.GetKeyDown(KeyCode.E)).Subscribe(_ =>
            {
                var unitSpawnData = new UnitSpawnData($"Enemy{UnityEngine.Random.Range(0, 1000)}", UnitTeam.EnemyTeam,true,UnitDBContainer.I.GetUnitSOByName("OctoAlien"));
                
                spawnUnitManager.TrySpawnTo(unitSpawnData,GetMousePosition());
            }).AddTo(this);  
            
            
            
            this.UpdateAsObservable().Where(_ => Input.GetKeyDown(KeyCode.W)).Subscribe(_ =>
            {
                var unitSpawnData = new UnitSpawnData($"Enemy{UnityEngine.Random.Range(0, 1000)}", UnitTeam.PlayerTeam,true,UnitDBContainer.I.GetUnitSOByName("Knight"));
                
                spawnUnitManager.TrySpawnTo(unitSpawnData,GetMousePosition());
            }).AddTo(this);  
            
            
            
            this.UpdateAsObservable().Where(_ => Input.GetKeyDown(KeyCode.R)).Subscribe(_ =>
            {
                var unitSO = UnitDBContainer.I.GetUnitSOByName("FatWhiteAlien");
                var unitSpawnData = new UnitSpawnData($"Enemy{UnityEngine.Random.Range(0, 1000)}", UnitTeam.EnemyTeam,true,unitSO);
                
                spawnUnitManager.TrySpawnTo(unitSpawnData,GetMousePosition());
            }).AddTo(this); 
            
            
            this.UpdateAsObservable().Where(_ => Input.GetKeyDown(KeyCode.B))
                .Subscribe(_ =>
                {
                    GameManager.I.GetPlayerModelByTeam(UnitTeam.PlayerTeam).TryUseSkill("Bomb", GetMousePosition());
                }).AddTo(this);
            
            this.UpdateAsObservable().Where(_ => Input.GetKeyDown(KeyCode.C))
                .Subscribe(_ =>
                {
                    GameManager.I.GetPlayerModelByTeam(UnitTeam.PlayerTeam).TryUseSkill("OctoAlien", GetMousePosition());
                }).AddTo(this);
        }
    }
}