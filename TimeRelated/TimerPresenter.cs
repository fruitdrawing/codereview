using System;
using UniRx;
using UnityEngine;

namespace DefaultNamespace.MyScripts.TimeRelated
{
    public class TimerPresenter : MonoBehaviour
    {
        public TimeManager Model;
        public TimerView View;

        private void Awake()
        {
            Model.CurrentRemaingSeconds.ObserveEveryValueChanged(x => x.Value)
                .Subscribe(_ =>
                {
                    View.TimerText.text =  TimeSpan.FromSeconds(_).ToString(@"m\:ss");
                }).AddTo(this);
        }
    }
}