using DefaultNamespace.MyScripts.GameData;
using DefaultNamespace.MyScripts.SharedData;
using UniRx;
using UnityEngine;

namespace DefaultNamespace.MyScripts
{
    public class CharacterStateHelper : MonoBehaviour
    {
        public UnitModel UnitModel;
        
        public AttackHelper AttackHelper;
        public MoveHelper MoveHelper;
        public DeathBehaviour DeathBehaviour;
        
        private void Awake()
        {
            AttackHelper.OnNoEnemyMoment.AsObservable().Subscribe(_ =>
            {
                Debug.Log($"<color=white>OnNoEnemyMoment</color>");
                if (GameOptionDB.I.DisablePlayerTeamUnitSettingDestinationAfterBattle == true &&
                    UnitModel.CurrentTeam.Value == UnitTeam.PlayerTeam) return;
                
                MoveHelper.SetDestination(GameManager.I.GetDestinationByTeam(UnitModel.CurrentTeam.Value),UnitModel.CurrentTeam.Value);
                Debug.Log($"<color=white>SetDestination : {GameManager.I.GetDestinationByTeam(UnitModel.CurrentTeam.Value)}</color>");
            }).AddTo(this);
            
            
            DeathBehaviour.OnDeath.AsObservable().Subscribe(_ =>
            {
                MoveHelper.Stop();
                AttackHelper.TryCancelCurrentAttacking();
                UnitModel.CurrentCharacterState.Value = CharacterState.Dead;
            }).AddTo(this);

            Observable.CombineLatest(
                    AttackHelper.AttackingReadyState, 
                    AttackHelper.AttackingFinishingState)
                .Subscribe(
                    _ =>
                    {
                        if (_.Contains(true))
                        {
                            MoveHelper.Stop();
                        }
                        else
                        {
                            MoveHelper.ResumeToMoveAvailable();
                        }
                    }).AddTo(this);
            
            // AttackHelper.AttackingReadyState
            //     .ObserveEveryValueChanged(
            //         x => x.Value)
            //     .Subscribe(_ =>
            //     {
            //         // if (_)
            //         // {
            //         //     MoveHelper.Stop();
            //         // }
            //         // else
            //         // {
            //         //     MoveHelper.ResumeToMoveAvailable();
            //         // }
            //     }).AddTo(this);
        }
    }
}