using System;
using DefaultNamespace.MyScripts.GamePlayerRelated;
using DefaultNamespace.MyScripts.SharedData;
using DefaultNamespace.MyScripts.SkillRelated;
using NaughtyAttributes;
using SharedData;
using UniRx;
using UnityEngine;

namespace DefaultNamespace.MyScripts.GamePlayAIRelated
{
    public class GamePlayAIManager : MonoBehaviour
    {
        public GamePlayerModel EnemyGamePlayerModel;
        
        private void Awake()
        {
            Observable.Interval(TimeSpan.FromSeconds(2)).Subscribe(_ =>
            {
                SpawnEnemyUnitTo();
            }).AddTo(this);
        }

        [Button]
        public void SpawnEnemyUnitTo()
        {
            SkillDbContainer.I.UseSkill(EnemyGamePlayerModel, "OctoAlien", GameManager.I.GetRandomSpawnablePositionByTeam(UnitTeam.EnemyTeam));
            // var octo = UnitDBContainer.I.GetUnitSOByName("OctoAlien");
            // var unitSpawnData = new UnitSpawnData()
            // {
            //     OwnerName = "1",
            //     Team = UnitTeam.EnemyTeam,
            //     MoveToDestinationImmediately = true,
            //     UnitSO = octo
            // };
            //
            // SpawnUnitManager.I.TrySpawnTo(unitSpawnData,GameManager.I.GetRandomSpawnablePositionByTeam(UnitTeam.EnemyTeam));
        }
    }
}