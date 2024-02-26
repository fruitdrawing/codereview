using System.Threading;
using Cysharp.Threading.Tasks;
using DefaultNamespace.MyScripts.SharedData;
using DefaultNamespace.MyScripts.VfxRelated;
using NaughtyAttributes;
using SharedData;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace.MyScripts
{
    public class AttackHelper : MonoBehaviour
    {
        public CharacterCore Owner;
        
        public SensorHelper SensorHelper;

        public ReactiveProperty<bool> AttackingMode = new();
        public ReactiveProperty<bool> AttackingReadyState = new();
        public ReactiveProperty<bool> AttackingFinishingState = new();

        public ReactiveProperty<float> CurrentAttackRange = new();
        
        public UnityEvent OnNoEnemyMoment = new();
        
        private CancellationTokenSource cts;
        
        private void Awake()
        {
               
            SensorHelper.OnAttackingTargetChanged.AsObservable().Subscribe(_ =>
            {
                if (_ == null)
                {
                    AttackingMode.Value = false;
                    if (cts != null)
                    {
                        cts.Cancel();
                    }
                    this.cts = new();
                    return;
                }
                else
                {
                    AttackingMode.Value = true;
                    return;
                }
            }).AddTo(this);

            
            AttackingMode.ObserveEveryValueChanged(x => x.Value)
                .Subscribe(_ =>
                {
                    Debug.Log($"<color=white>AttackingMode : {_}</color>");

                    if (_ == true)
                    {
                        CancelAndRefreshToken();
                        KeepAttacking(cts.Token);
                    }
                    else
                    {
                        CancelAndRefreshToken();
                        SensorHelper.RefreshAttackingTarget();
                        
                        if (SensorHelper.GetCurrentAttackingTarget() == null)
                        {
                            Reset();
                            OnNoEnemyMoment?.Invoke();
                        }
                        else
                        {
                            
                        }
                    }
                }).AddTo(this);
        }

        private void CancelAndRefreshToken()
        {
            if (cts != null)
            {
                cts.Cancel();
            }
            this.cts = new();
            AttackingReadyState.Value = false;
            AttackingFinishingState.Value = false;
        }

        [Button]
        public void TryCancelCurrentAttacking()
        {
            Debug.Log($"<color=white>TryCancelCurrentAttacking!!</color>");
            CancelAndRefreshToken();
            Reset();
        }
        
        private async UniTask KeepAttacking(CancellationToken ct)
        {
            while(ct.IsCancellationRequested == false)
            {
                if (Owner.UnitModel.CurrentAttackType == AttackType.Melee)
                {
                    await TryAttackSingleTargetWithAnimation(
                        SensorHelper.GetCurrentAttackingTarget(),
                        Owner.UnitModel.CreateDamageDataByUnitModel(),
                        0.4f,0.8f,"Blue Slash v16",
                        ct);
                }
                else if(Owner.UnitModel.CurrentAttackType == AttackType.Range)
                {
                    await TryAttackSingleTargetWithAnimation(
                        SensorHelper.GetCurrentAttackingTarget(), 
                        Owner.UnitModel.CreateDamageDataByUnitModel(),
                        0.3f,0.5f,"Arrow",
                        ct);
                }
             
            }
        }

        public async UniTask TryAttackSingleTargetWithAnimation(CharacterCore targetCharacterCore, DamageData damageData,
            float readyAnimationDuration, float afterFireDuration,string vfxName,CancellationToken ct)
        {
            if (targetCharacterCore == null)
            {
                await UniTask.Delay((int) (readyAnimationDuration * 1000),cancellationToken:ct);
                return;
            }
            if(Vector2.Distance(targetCharacterCore.transform.position,Owner.transform.position) > CurrentAttackRange.Value)
            {
                await UniTask.Delay((int) (readyAnimationDuration * 1000),cancellationToken:ct);
                return;
            }

            if (vfxName == "Arrow")
            {
                MyVfxManager.I.ShootArrow(Owner.transform.position, targetCharacterCore.transform.position,readyAnimationDuration);
            }
            else if(vfxName == "Blue Slash v16")
            {
                var spawned= MyVfxManager.I.CreateVfxByName("Blue Slash v16", Owner.transform.position,0.6f);
                // spawned.DOLookAt(targetCharacterCore.transform.position,0f);
            }
            
            Debug.Log($"<color=white>Actual Attack Start!</color>");

            
            AttackingReadyState.Value = true;
            await UniTask.Delay((int) (readyAnimationDuration * 1000),cancellationToken:ct);
            TryInstantAttackToSingleTarget(targetCharacterCore, damageData);
            AttackingReadyState.Value = false;
            AttackingFinishingState.Value = true;

            await UniTask.Delay((int) (afterFireDuration * 1000),cancellationToken:ct);
            AttackingFinishingState.Value = false;
        }

        private void Reset()
        {
            AttackingFinishingState.Value = false;
            AttackingMode.Value = false;
            AttackingReadyState.Value = false;
        }
        
        public void TryInstantAttackToSingleTarget(CharacterCore targetCharacterCore, DamageData damageData)
        {
            if (targetCharacterCore == null) return;
            targetCharacterCore.GetTakeDamageHelper().TakeDamage(damageData);
        }
        
    }
}