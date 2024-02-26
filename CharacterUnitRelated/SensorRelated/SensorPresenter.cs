using DefaultNamespace.MyScripts.UtilClasses;
using UniRx;
using UnityEngine;

namespace DefaultNamespace.MyScripts
{
    public class SensorPresenter : MonoBehaviour
    {
        public UnitModel Model;
        public SensorHelper View;

        private void Awake()
        {
            Model.DetectEnemyUnitRange.ObserveEveryValueChanged(x => x.Value)
                .Subscribe(_ =>
                {
                    View.CurrentEnemyDetectRadiusSensor.Value = _;
                }).AddTo(this);

            Model.OnUnitSpanwed.AsObservable().Subscribe(_ =>
            {
                View.InitByUnitDataSO(_.Team.GetOppositeTeam());
            }).AddTo(this);
        }
    }
}