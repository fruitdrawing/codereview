using System;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace DefaultNamespace.MyScripts
{
    public class UltimateShaderHelper : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer SpriteRenderer;
        
        
        
        
        private Material _targetMaterial;
        private Sequence hitEffectSequence;
        
        private void Awake()
        {
            SpriteRenderer.material = new( SpriteRenderer.material);
            _targetMaterial = SpriteRenderer.material;
            hitEffectSequence = DOTween.Sequence();
            hitEffectSequence.Pause();
            hitEffectSequence.SetAutoKill(false);
            hitEffectSequence.Append(SpriteRenderer.material.DOFloat(1,"_MaskAmount", 0));
            hitEffectSequence.Append(SpriteRenderer.material.DOFloat(0,"_MaskAmount", 0.05f));
        }

        [Button]
        public void HitEffect()
        {
            hitEffectSequence.Restart();
        }
    }
}