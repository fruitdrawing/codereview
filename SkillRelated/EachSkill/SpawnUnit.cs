using Cysharp.Threading.Tasks;
using DefaultNamespace.MyScripts.SharedData;
using SharedData;
using UnityEngine;

namespace DefaultNamespace.MyScripts.SkillRelated.EachSkill
{
    [CreateAssetMenu(fileName = "SpawnUnit", menuName = "SkillSO/SpawnUnit", order = 0)]
    public class SpawnUnit : SkillModel
    {
        public override Sprite GetSkillIcon()
        {
            return SkillImplementData.UnitSO.UnitSprite;
        }
        public override async UniTask<SkillResultData> TryUseSkill(UnitTeam team,Vector3 position)
        {
            var spawn =new UnitSpawnData()
            {
                OwnerName = "123",
                UnitSO = SkillImplementData.UnitSO,
                Team = team,
                ManaRequiredToSpawn = SkillImplementData.ManaCost,
                MoveToDestinationImmediately = true
            };
            Debug.Log($"<color=white>{position}</color>");
            SpawnUnitManager.I.TrySpawnTo(spawn, position);
            return new SkillResultData();
        }
    }
}