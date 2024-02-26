using System;
using DefaultNamespace.MyScripts.SharedData;
using DefaultNamespace.MyScripts.SkillRelated;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace.MyScripts.GamePlayerRelated
{
    public class GamePlayerModel : MonoBehaviour
    {
        public ReactiveProperty<string> NameOfUser = new();
        
        public UnitTeam Team;
        
        public ReactiveProperty<int> CurrentHp = new();
        public ReactiveProperty<int> MaxHp = new();
        
        
        public ReactiveProperty<int> CurrentMana = new(100);
        public ReactiveProperty<int> MaxMana = new(100);

        public ReactiveProperty<float> CurrentManaRatio = new(1);
        
        public ReactiveProperty<int> CurrentMoney = new(0);


        private void Awake()
        {
            CurrentMana.ObserveEveryValueChanged(x => x.Value)
                .Subscribe(_ =>
                {
                    if(_ > MaxMana.Value)
                    {
                        CurrentMana.Value = MaxMana.Value;
                    }
                    if (_ <= 0)
                    {
                        CurrentManaRatio.Value = 0;
                        return;
                    }
                    CurrentManaRatio.Value = (float)_ / MaxMana.Value;
                }).AddTo(this);

            SubscirbeAddingMana();
            
        }

        private void SubscirbeAddingMana()
        {
            Observable.Interval(TimeSpan.FromSeconds(1)).Subscribe(_ =>
            {
                AddCurrentMana(1);
            }).AddTo(this);
        }

        public void AddCurrentMana(int amount)
        {
            CurrentMana.Value += amount;
        }

        public void AddMoney(int amountMoney)
        {
            CurrentMoney.Value += amountMoney;
        }

        public bool TryUseMoney(int amountMoney)
        {
            if (CurrentMoney.Value < amountMoney)
            {
                return false;
            }
            CurrentMoney.Value -= amountMoney;
            return true;
        }
        
        public void TryUseSkill(string skillName, Vector3 position)
        {
            SkillDbContainer.I.UseSkill(this,skillName, position);
        }
        

        public void UseMana(int manaCost)
        {
            CurrentMana.Value -= manaCost;
        }
    }
}