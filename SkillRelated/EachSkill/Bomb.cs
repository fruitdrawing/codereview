using Cysharp.Threading.Tasks;
using DefaultNamespace.MyScripts.AudioRelated;
using DefaultNamespace.MyScripts.SharedData;
using UnityEngine;
using UtilClasses;

namespace DefaultNamespace.MyScripts.SkillRelated.EachSkill
{
    [CreateAssetMenu(fileName = "Bomb", menuName = "SkillSO/Bomb", order = 0)]
    public class Bomb : SkillModel
    {
        public override async UniTask<SkillResultData> TryUseSkill(UnitTeam team,Vector3 position)
        {
            var a = new SkillResultData();
            var worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPoint = new Vector3(worldPoint.x, worldPoint.y, 0);
            AudioManager.I.PlaySfx("Explosion - 011");
            var units = SkillAimHelper.I.GetCharacterCoreInCircle(worldPoint, 2f, TagHelper.AllTeams(), null);
            foreach (var VARIABLE in units)
            {
                VARIABLE.GetTakeDamageHelper().TakeDamage(new DamageData(10,2,0.3f));
            }
            
            return a;

        }
    }
}