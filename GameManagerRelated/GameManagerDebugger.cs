using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace DefaultNamespace.MyScripts
{
    public class GameManagerDebugger : MonoBehaviour
    {
        private void Awake()
        {
            this.UpdateAsObservable().Where(_ => Input.GetKeyDown(KeyCode.Q))
                .Subscribe(_ =>
                {
                    GameManager.I.PlayerTeamPlayerModel.AddCurrentMana(100);
                }).AddTo(this);
        }
    }
}