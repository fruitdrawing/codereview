using System;
using DefaultNamespace.MyScripts.SharedData;
using DG.Tweening;
using NaughtyAttributes;
using UniRx;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace.MyScripts
{
    public class TakeDamageHelper : MonoBehaviour
    {
        public ReactiveDictionary<string, float> CurrentKnockbackList = new();
        public UnityEvent<int> OnTakeDamage = new();
        public ReactiveProperty<bool> OnKnockbacking = new();
        public UnitModel unitModel;


        private void Awake()
        {
            CurrentKnockbackList.ObserveCountChanged().Subscribe(_ =>
            {
                if (CurrentKnockbackList.Count > 0)
                {
                    OnKnockbacking.Value = true;
                        
                }
                else
                {
                    OnKnockbacking.Value = false;

                }
            }).AddTo(this);
        }

        [Button]
        public void DebugTakeDamage10()
        {
            TakeDamage(new DamageData(10, 0,0));
        }

        [Button]
        public void Heal10()
        {
            Heal(10);
        }
        public void TakeDamage(DamageData damageData)
        {
            unitModel.CurrentHp.Value -= damageData.DamageAmount;
            if (damageData.PushPower > 0)
            {

                var newGuid = Guid.NewGuid();
                CurrentKnockbackList.Add(newGuid.ToString(), damageData.PushPower);
                DOVirtual.DelayedCall(damageData.PushDuration,() => CurrentKnockbackList.Remove(newGuid.ToString()));
            }
            OnTakeDamage?.Invoke(damageData.DamageAmount);
        }

        public void Heal(int healAmount)
        {
            unitModel.CurrentHp.Value += healAmount;
        }
        

    }
}