using DefaultNamespace.MyScripts.VfxRelated;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace.MyScripts
{
    public class DeathBehaviour : MonoBehaviour
    {
        [SerializeField]
        private UnitModel unitModel; 
        
        [SerializeField]
        private CharacterCore _characterCore;

        public ReactiveProperty<bool> IsDead = new(false);
        public UnityEvent<UnitModel> OnDeath = new();
        
        
        private void Awake()
        {
            OnDeath.AsObservable().Subscribe(_ =>
            {
                MyVfxManager.I.CreateVfxByName("Particles_Basic_V2", _characterCore.transform.position, 2f);
            }).AddTo(this);
            
            unitModel.CurrentHp.ObserveEveryValueChanged(x => x.Value)
                .Where(_ => _ == 0)
                .Subscribe(_ =>
                {
                    Debug.Log($"{unitModel.OwnerName} : I'm dead!");
                    IsDead.Value = true;
                    OnDeath?.Invoke(unitModel);
                    SpawnUnitManager.I.DespawnUnitByInstanceId(_characterCore.gameObject);
                }).AddTo(this);
        }
    }
}