using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.MyScripts.SharedData;
using NaughtyAttributes;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace.MyScripts
{
    public class SensorHelper : MonoBehaviour
    {
        public ReactiveProperty<bool> SensorAvailable = new(true);

        [SerializeField]
        private ReactiveProperty<CharacterCore> CurrentAttackingTarget = new();

        public List<GameObject> CurrentSensingTargets = new();
        
        public ReactiveProperty<float> CurrentEnemyDetectRadiusSensor = new(2);
        public ReactiveProperty<float> CurrentAttackDetectRadiusSensor = new(2);

        public ReactiveCollection<GameObject> CurrentSensingCharacterList = new();
        
        // References
        public CircleCollider2D SensorCircleCollider2D;

        // Values
        [Space(20)]
        public UnityEvent<CharacterCore> OnAttackingTargetChanged = new();
        
        private IDisposable sensorEnterSub;
        private IDisposable sensorExitSub;
        private IDisposable onTargetUnitDeathSubscription;

        public CharacterCore GetCurrentAttackingTarget()
        {
            if (CurrentAttackingTarget.Value != null)
            {
                if (CurrentAttackingTarget.Value.gameObject.activeSelf == true)
                {
                    return CurrentAttackingTarget.Value;
                }
                else
                {
                    return null;
                }
            }
            return CurrentAttackingTarget.Value;
        }
        public bool TryGetCurrentAttackingTarget(out CharacterCore result)
        {
            result = null;
            if (CurrentAttackingTarget.Value == null) return false;
            result = CurrentAttackingTarget.Value;
            return true;
        }
        
        private void Awake()
        {
            CurrentAttackingTarget.ObserveEveryValueChanged(x => x.Value)
                .Subscribe(_ =>
                {
                    onTargetUnitDeathSubscription?.Dispose();
                    if (_ != null)
                    {
                        onTargetUnitDeathSubscription = _.DeathBehaviour.OnDeath.AsObservable().Subscribe(__ =>
                        {
                            CurrentAttackingTarget.Value = null;
                            OnAttackingTargetChanged?.Invoke(null);
                        }).AddTo(_);
                    }
                }).AddTo(this);
            CurrentSensingCharacterList.ObserveCountChanged().Subscribe(_ =>
            {
                CurrentSensingTargets.Clear();
                foreach (var VARIABLE in CurrentSensingCharacterList)
                {
                    CurrentSensingTargets.Add(VARIABLE);
                }
            }).AddTo(this);
            
            
            CurrentEnemyDetectRadiusSensor.ObserveEveryValueChanged(x => x.Value)
                .Where(_ => SensorAvailable.Value == true)
                .Subscribe(_ =>
                {
                    SensorCircleCollider2D.radius = _;
                }).AddTo(this);

            CurrentSensingCharacterList.ObserveAdd().Subscribe(_ =>
            {
                RefreshAttackingTarget();
            }).AddTo(this);
            
            CurrentSensingCharacterList.ObserveRemove().Subscribe(_ =>
            {
                if (CurrentSensingCharacterList.Count == 0)
                {
                    CurrentAttackingTarget.Value = null;
                    OnAttackingTargetChanged?.Invoke(null);
                }
            }).AddTo(this);
            //
            //
            // this.UpdateAsObservable()
            //     .Subscribe(_ =>
            // {
            //     if (CurrentSensingCharacterList.Count > 0)
            //     {
            //         var orderByDistance =
            //             CurrentSensingCharacterList.OrderBy(
            //                 x => Vector2.Distance(this.transform.position, x.transform.position)).ToList();
            //         
            //         foreach (var VARIABLE in orderByDistance)
            //         {
            //             Debug.Log($"<color=white>{VARIABLE.name}</color>");
            //         }
            //         
            //         CurrentAttackingTarget.Value = orderByDistance.First().gameObject.GetComponent<CharacterCore>();
            //         OnAttackingTargetChanged?.Invoke(CurrentAttackingTarget.Value);
            //         
            //     }
            //     else
            //     {
            //         CurrentAttackingTarget.Value = null;
            //         OnAttackingTargetChanged?.Invoke(null);
            //     }
            // }).AddTo(this);


        }

        public void RefreshAttackingTarget()
        {
            if (CurrentSensingCharacterList.Count == 0) return;
            
            var orderByDistance =
                CurrentSensingCharacterList.OrderBy(
                    x => Vector2.Distance(this.transform.position, x.transform.position)).ToList();

            foreach (var VARIABLE in orderByDistance)
            {
                Debug.Log($"<color=white>{VARIABLE.name}</color>");
            }
            CurrentAttackingTarget.Value = orderByDistance.First().gameObject.GetComponent<CharacterCore>();
            OnAttackingTargetChanged?.Invoke(CurrentAttackingTarget.Value);
        }

        [Button]
        public void SensorToEnemyTeam()
        {
            InitByUnitDataSO(UnitTeam.EnemyTeam);
        }
        
        [Button]
        public void SensingPlayerTeam()
        {
            InitByUnitDataSO(UnitTeam.PlayerTeam);
        }
        
        

        public void InitByUnitDataSO(UnitTeam desiredSensingTeam)
        {
            // disposables.Dispose();
            
            if(sensorEnterSub != null) sensorEnterSub.Dispose();
            sensorEnterSub = SensorCircleCollider2D.OnTriggerEnter2DAsObservable()
                .Where(_ => _.gameObject.CompareTag(desiredSensingTeam.ToString()))
                .Where(_ => SensorAvailable.Value == true)
                .Subscribe(_ =>
                {
                    if (CurrentSensingCharacterList.Contains(_.gameObject) == false)
                    {
                        CurrentSensingCharacterList.Add(_.gameObject);
                    }
                    
                    // Debug.Log($"<color=red>SENSORED! from : {this.gameObject.name} which unit :  {_.gameObject.name}</color>");
                    // var foundCore = _.gameObject.GetComponent<CharacterCore>();
                    // foundCore.DeathBehaviour.OnDeath.AsObservable().Subscribe(_ =>
                    // {
                    //     CurrentAttackingTarget.Value = null;
                    //     Debug.Log($"<color=white>foundCORE DEATH</color>");
                    //     OnAttackingTargetChanged?.Invoke(CurrentAttackingTarget.Value);
                    //
                    // }).AddTo(foundCore.gameObject);
                    //
                    // CurrentAttackingTarget.Value = foundCore;
                    // OnAttackingTargetChanged?.Invoke(CurrentAttackingTarget.Value);
                }).AddTo(this);

            
            if (sensorExitSub != null) sensorExitSub.Dispose();
            sensorExitSub = SensorCircleCollider2D.OnTriggerExit2DAsObservable()
                .Where(_ => _.gameObject.CompareTag(desiredSensingTeam.ToString()))
                .Where(_ => SensorAvailable.Value == true)
                .Subscribe(_ =>
                {
                    if (CurrentSensingCharacterList.Contains(_.gameObject))
                    {
                        CurrentSensingCharacterList.Remove(_.gameObject);
                        if(CurrentSensingCharacterList.Count == 0)
                        {
                            CurrentAttackingTarget.Value = null;
                            OnAttackingTargetChanged?.Invoke(null);
                        }
                    }
                }).AddTo(this);

        }

        
    }
}