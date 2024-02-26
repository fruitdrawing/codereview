using MyDomain.Utils;
using UniRx;
using UnityEngine;

namespace DefaultNamespace.MyScripts.GameData
{
    [CreateAssetMenu(fileName = "GameOptionDB", menuName = "DB/GameOptionDB", order = 0)]
    public class GameOptionDB : SingletonScriptableObject<GameOptionDB>
    {
        public ReactiveProperty<bool> ShowHPBar;
        
        public bool ShowUnitDebugInfo;
        public bool DisablePlayerTeamUnitSettingDestinationAfterBattle;
        
        public ReactiveProperty<bool> ShowNameTextTMP = new();
    }
}