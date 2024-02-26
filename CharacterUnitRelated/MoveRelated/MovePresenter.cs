using DefaultNamespace.MyScripts.GameData;
using DefaultNamespace.MyScripts.SharedData;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace DefaultNamespace.MyScripts.MoveRelated
{
    public class MovePresenter : MonoBehaviour
    {
        public UnitModel Model;
        public MoveHelper View;
        public SensorHelper SensorModel;
        
        private void Awake()
        {

            
            Model.DisableRVO.ObserveEveryValueChanged(x => x.Value)
                .Subscribe(_ =>
                {
                    View.RVOController.enabled = !_;
                }).AddTo(this);
            
            Model.UnitBodyRadius.ObserveEveryValueChanged(x => x.Value)
                .Subscribe(_ =>
                {
                    View.AIPath.radius = _;
                }).AddTo(this);
            
            
            Model.DisableMovement.ObserveEveryValueChanged(x => x.Value)
                .Subscribe(_ =>
                {
                    View.AllowSetDestination.Value = !_;
                }).AddTo(this);
            
            Model.MovementSpeed.ObserveEveryValueChanged(x => x.Value)
                .Subscribe(_ =>
                {
                    View.AIPath.maxSpeed = _;
                }).AddTo(this);
            
            this.UpdateAsObservable().Subscribe(_ =>
            {
                if (View.AIPath.desiredVelocity.x < 0)
                {
                    Model.CurrentDirection.Value = CharacterDirection.Left;
                }
                else
                {
                    Model.CurrentDirection.Value = CharacterDirection.Right;
                }
            }).AddTo(this);

            
            Model.OnUnitSpanwed.AsObservable().Subscribe(_ =>
            {
                if (_.MoveToDestinationImmediately == true)
                {
                    if (GameOptionDB.I.DisablePlayerTeamUnitSettingDestinationAfterBattle == true &&
                        _.Team == UnitTeam.PlayerTeam)
                    {
                        return;
                    }
                    View.SetDestination(GameManager.I.GetDestinationByTeam(_.Team),_.Team);
                }
            }).AddTo(this);

            
            
            
            SensorModel.OnAttackingTargetChanged.AsObservable().Subscribe(newTarget =>
            {
                if (newTarget == null)
                {
                    // View.Stop();
                }
                else
                {
                    View.SetDestination(newTarget.transform,Model.CurrentTeam.Value);                
                }
            }).AddTo(this);
        }
    }
}