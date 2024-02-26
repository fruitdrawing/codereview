using UniRx;
using UnityEngine;

namespace DefaultNamespace.MyScripts
{
    public class AnimationPresenter : MonoBehaviour
    {
        public AnimationView View;
        public TakeDamageHelper Model;
        public UltimateShaderHelper ShaderView;
        
        private void Awake()
        {
            Model.OnTakeDamage.AsObservable().Subscribe(damageAmount =>
            {
                ShaderView.HitEffect();
            }).AddTo(this);
        }
    }
}