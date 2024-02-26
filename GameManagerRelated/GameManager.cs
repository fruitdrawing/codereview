using DefaultNamespace.MyScripts.GamePlayerRelated;
using DefaultNamespace.MyScripts.SharedData;
using DefaultNamespace.MyScripts.UtilClasses;
using DG.Tweening;
using SharedData;
using UnityEngine;

namespace DefaultNamespace.MyScripts
{
    public class GameManager : Singleton<GameManager>
    {
        public GamePlayerModel PlayerTeamPlayerModel;
        public GamePlayerModel EnemyTeamPlayerModel;

        public BoxCollider2D PlayerTeamSpawnArea;
        public BoxCollider2D EnemyTeamSpawnArea;

        
        
        public GamePlayerModel GetPlayerModelByTeam(UnitTeam team)
        {
            return team == UnitTeam.EnemyTeam ? EnemyTeamPlayerModel : PlayerTeamPlayerModel;
        }
        
        [SerializeField]
        private Transform EnemyTeamDestination;
        [SerializeField]
        private Transform PlayerTeamDestination;

        
        private void Awake()
        {
            SpawnGamePlayerUnits();
            SpawnPlayerInput();
        }


        public Vector3 GetRandomSpawnablePositionByTeam(UnitTeam team)
        {
            if (team == UnitTeam.EnemyTeam)
            {
                return EnemyTeamSpawnArea.GetRandomPositionInsideCollider();
            }
            else
            {
                return PlayerTeamSpawnArea.GetRandomPositionInsideCollider();
            }
        }

        public void SpawnPlayerInput()
        {
            var fitness = UnitDBContainer.I.GetUnitSOByName("FitnessYouTuber");
            var spawnDATAPlayer = new UnitSpawnData()
            {
                Team = UnitTeam.PlayerTeam,
                MoveToDestinationImmediately = false,
                UnitSO = fitness,
                IsPlayerController = true
            };  
            SpawnUnitManager.I.TrySpawnTo(spawnDATAPlayer, GetDestinationByTeam(UnitTeam.EnemyTeam).position);

        }
        private void SpawnGamePlayerUnits()
        {
            DOVirtual.DelayedCall(0.5f, () =>
            {
                var fitness = UnitDBContainer.I.GetUnitSOByName("FitnessYouTuber");
                var FemaleIdolPink = UnitDBContainer.I.GetUnitSOByName("FemaleIdolPink");
                var spawnDATAPlayer = new UnitSpawnData()
                {
                    Team = UnitTeam.PlayerTeam,
                    MoveToDestinationImmediately = false,
                    UnitSO = fitness
                };  
                
                var spawnDATAEnemy = new UnitSpawnData()
                {
                    Team = UnitTeam.EnemyTeam,
                    MoveToDestinationImmediately = false,
                    UnitSO = FemaleIdolPink
                };
                
                SpawnUnitManager.I.TrySpawnTo(spawnDATAPlayer, GetDestinationByTeam(UnitTeam.EnemyTeam).position);
                SpawnUnitManager.I.TrySpawnTo(spawnDATAEnemy, GetDestinationByTeam(UnitTeam.PlayerTeam).position);
            });
        }


        public Transform GetDestinationByTeam(UnitTeam team)
        {
            if (team == UnitTeam.EnemyTeam)
                return EnemyTeamDestination;
            else
            return PlayerTeamDestination;
        }


        public void AddMoney(int amountMOney,UnitTeam team)
        {
            GetPlayerModelByTeam(team).AddMoney(amountMOney);
        }
    }
}