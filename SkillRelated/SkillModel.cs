using Cysharp.Threading.Tasks;
using DefaultNamespace.MyScripts.SharedData;
using NaughtyAttributes;
using UnityEngine;

namespace DefaultNamespace.MyScripts.SkillRelated
{
    public abstract class SkillModel : ScriptableObject
    {
        [ShowAssetPreview(20)]
        [SerializeField]
        private Sprite _icon;
        
        [SerializeField] public SkillImplementData SkillImplementData = new();


        public virtual Sprite GetSkillIcon()
        {
            return _icon;
        }
        public string GetSkillName()
        {
            return name;
        }

        public abstract UniTask<SkillResultData> TryUseSkill(UnitTeam team,Vector3 position);
    }
}