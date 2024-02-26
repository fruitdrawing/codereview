using DefaultNamespace.MyScripts.SharedData;
using NaughtyAttributes;
using SharedData;
using UnityEngine;

namespace DefaultNamespace.MyScripts
{
    [CreateAssetMenu(fileName = "UnitSO", menuName = "Spawn/UnitSO", order = 0)]
    public class UnitSO : ScriptableObject
    {
        public Sprite UnitSprite;
        
        public string UnitName => this.name;
        public int InitialHp;
        public int MaxHp;

        public int ManaRequiredToSpawn;
        
        public int AttackPower;
        public float PushPower;
        public float PushDuration;
        
        public float AttackRange;
        public float DetectEnemyUnitRange;
        public AttackType AttackType;
        
        public bool AirUnit;
        public float Radius;

        public float CharacterGraphicHeight;
        
        public bool DisableRVO;
        public bool DisableMovement;
        public float MoveSpeed;

        [Button]
        public void DebugSelfSpawnPlayer()
        {
            var spawnData = new UnitSpawnData()
            {
                Team = UnitTeam.PlayerTeam,
                UnitSO = this,
                OwnerName = this.name,
                MoveToDestinationImmediately = true,
                ManaRequiredToSpawn = ManaRequiredToSpawn

                
            };
            SpawnUnitManager.I.TrySpawnTo(spawnData,Vector3.zero);
        }
        
        [Button]
        public void DebugSelfSpawnEnemy()
        {
            var spawnData = new UnitSpawnData()
            {
                Team = UnitTeam.EnemyTeam,
                UnitSO = this,
                OwnerName = this.name,
                MoveToDestinationImmediately = true,
                ManaRequiredToSpawn = ManaRequiredToSpawn
            };
            SpawnUnitManager.I.TrySpawnTo(spawnData,Vector3.zero);
        }
    }
}