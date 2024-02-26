using System;
using DefaultNamespace.MyScripts.GameData;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace.MyScripts
{
    public class PlayerUnitView : MonoBehaviour
    {
        public UnitModel unitModel;
        public TextMeshProUGUI TextMeshProUGUI;

        private void Awake()
        {
            GameOptionDB.I.ShowNameTextTMP.ObserveEveryValueChanged(x => x.Value)
                .Subscribe(_ =>
                {
                    TextMeshProUGUI.gameObject.SetActive(_);
                }).AddTo(this);
            
            unitModel.OwnerName.ObserveEveryValueChanged(x => x.Value)
                .Subscribe(_ =>
                {
                    TextMeshProUGUI.text = _.ToString();
                }).AddTo(this);
            unitModel.HPRatio.ObserveEveryValueChanged(x => x.Value)
                .Subscribe(_ =>
                {
                    if (_ > 0.8f)
                    {
                        TextMeshProUGUI.color = Color.white;
                    }
                    else if (_ <= 0.8f && _ > 0.3f)
                    {
                        TextMeshProUGUI.color = Color.yellow;
                    }  
                    else if (_ <= 0.3f)
                    {
                        TextMeshProUGUI.color = Color.red;
                    }
                }).AddTo(this);
            
        }
    }
}