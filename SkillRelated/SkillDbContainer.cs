using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DefaultNamespace.MyScripts.GamePlayerRelated;
using MyDomain.Utils;
using NaughtyAttributes;
using SharedData;
using UnityEngine;

namespace DefaultNamespace.MyScripts.SkillRelated
{
    [CreateAssetMenu(fileName = "SkillDbContainer", menuName = "DB/SkillDbContainer", order = 0)]
    public class SkillDbContainer : SingletonScriptableObject<SkillDbContainer>, IHasResourcesToLoad
    {
        public List<SkillModel> SkillModels = new();
        public SkillModel GetSkillModel(string skillName)
        {
            return SkillModels.Find(skillModel => skillModel.GetSkillName() == skillName);
        }
        
        public void UseSkill(GamePlayerModel caster,string skillName,Vector3 position)
        {
            var skillModel = GetSkillModel(skillName);
            if (caster.CurrentMana.Value > skillModel.SkillImplementData.ManaCost)
            {
                caster.UseMana(skillModel.SkillImplementData.ManaCost);
                skillModel.TryUseSkill(caster.Team,position).Forget();
                return;
            }

            Debug.Log($"<color=white>Not enough mana</color>");
        }


        [Button]
        public void LoadResources()
        {
            SkillModels = new();
            SkillModels.AddRange( Resources.LoadAll<SkillModel>("SkillSO/"));
            SetDirty();
        }
    }
}