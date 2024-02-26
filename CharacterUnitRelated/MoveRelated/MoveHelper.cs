using DefaultNamespace.MyScripts.GameData;
using DefaultNamespace.MyScripts.SharedData;
using DefaultNamespace.MyScripts.VfxRelated;
using NaughtyAttributes;
using Pathfinding;
using Pathfinding.RVO;
using UniRx;
using UnityEngine;

namespace DefaultNamespace.MyScripts
{
    public class MoveHelper : MonoBehaviour
    {
        public ReactiveProperty<bool> AllowSetDestination = new();
        public ReactiveProperty<bool> AvailableMovingByAIPath = new(true);
        
        public Vector3 Debug_TargetDestination;
        
        // AIPATH는 Seeker가 찾은 Path를 베이스로 글루 이동하는 Movement Script.
        public AIPath AIPath;

        public RVOController RVOController;
        
        // SEEKER가 새로운 Path를 계산하는것.
        public Seeker Seeker;
        
        public AIDestinationSetter AIDestinationSetter;

        [Button]
        public void REPORT()
        {
            var path = Seeker.GetCurrentPath();
            path.vectorPath.ForEach(_ =>
            {
                Debug.Log($"<color=white>{_.ToString()}</color>");
            });
            foreach (var v3 in path.vectorPath)
            {
                MyVfxManager.I.CreateVfxByName("VFX_CircleAttackRange", v3, 1);
                Debug.Log($"<color=white>{v3.ToString()}</color>");
            }
        }
        public void SetDestination(Transform target,UnitTeam teamOfUnit)
        {
            if (AllowSetDestination.Value == false) return;
            if (GameOptionDB.I.DisablePlayerTeamUnitSettingDestinationAfterBattle &&
                teamOfUnit == UnitTeam.PlayerTeam) return;
            
            Debug.Log($"<color=white>SetDestination : {target.gameObject.name}</color>");
            AIDestinationSetter.target = target;
        }
        
        private void Awake()
        {
            AvailableMovingByAIPath.ObserveEveryValueChanged(x => x.Value)
                .Subscribe(_ =>
                {
                    AIPath.canMove = _;
                }).AddTo(this);
            
            
        }
        
        [Button]
        public void Stop()
        {
            AvailableMovingByAIPath.Value = false;
        }
        
        [Button]
        public void ResumeToMoveAvailable()
        {
            AvailableMovingByAIPath.Value = true;
            Debug.Log("ResumeToMoveAvailable");

        }
    }
}