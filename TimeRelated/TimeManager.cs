using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DefaultNamespace.MyScripts.UtilClasses;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace DefaultNamespace.MyScripts.TimeRelated
{
    public class TimeManager : Singleton<TimeManager>
    {
        public ReactiveProperty<float> CurrentTimeScale = new(1);
        public ReactiveProperty<int> CurrentRemaingSeconds = new(300);


        private CancellationToken cts;
        
        private async UniTask StartTimer(CancellationToken ct)
        {
            while (CurrentRemaingSeconds.Value > 0)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(1),cancellationToken:ct);
                CurrentRemaingSeconds.Value -= 1;
                
            }
            
        }
        private void Awake()
        {

            cts = new();
            StartTimer(cts);
            
            this.UpdateAsObservable().Select(_ => Time.timeScale).Subscribe(_ =>
            {
                CurrentTimeScale.Value = _;
            }).AddTo(this);


            CurrentTimeScale.ObserveEveryValueChanged(x => x.Value)
                .Subscribe(_ =>
                {
                    Debug.Log($"<color=white>Current TimeScale : {_}</color>");
                }).AddTo(this);
            this.UpdateAsObservable().Where(_ => Input.GetKeyDown(KeyCode.Minus))
                .Subscribe(_ =>
                {
                    Time.timeScale -= 0.5f;
                }).AddTo(this);
            
            
            
            this.UpdateAsObservable().Where(_ => Input.GetKeyDown(KeyCode.Equals))
                .Subscribe(_ =>
                {
                    Time.timeScale += 0.5f;
                }).AddTo(this);       
            
            this.UpdateAsObservable().Where(_ => Input.GetKeyDown(KeyCode.Alpha0))
                .Subscribe(_ =>
                {
                    
                    Time.timeScale = 1;
                }).AddTo(this);
        }
    }
}