using UniRx;
using UnityEngine;

namespace DefaultNamespace.MyScripts
{
    public class AttackPresenter : MonoBehaviour
    {
        public UnitModel Model;
        public AttackHelper View;
        
        private void Awake()
        {
            Model.AttackRange.ObserveEveryValueChanged(x => x.Value)
                .Subscribe(_ =>
                {
                    View.CurrentAttackRange.Value = _;
                }).AddTo(this);
            
        }
    }
}