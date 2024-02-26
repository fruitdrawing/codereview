using System;
using Cysharp.Threading.Tasks;
using DefaultNamespace.MyScripts.SharedData;
using DefaultNamespace.MyScripts.VfxRelated;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace.MyScripts
{
    public class AnimationView : MonoBehaviour
    {
        public Transform SpriteParent;
        
        public SpriteRenderer CharacterSpriteRenderer;
        public SpriteRenderer ShadowSpriteRenderer;

        // REFERENCE
        public TakeDamageHelper TakeDamageHelper;


        private Tween CurrentAnimation;
        
        public UnitModel unitModel;
        private void Awake()
        {

            unitModel.OnUnitSpanwed.AsObservable().Subscribe(_ =>
            {
                SpawnAnimation();
            }).AddTo(this);
            
            unitModel.CharacterGraphicHeight.ObserveEveryValueChanged(x => x.Value)
                .Subscribe(_ =>
                {
                    CharacterSpriteRenderer.transform.localPosition = new Vector3(0, _, 0);
                }).AddTo(this);
            
            TakeDamageHelper.OnTakeDamage.AsObservable().Subscribe(_ =>
            {
                OnTakeDamageSpriteEffect(_);
            }).AddTo(this);         
            
            
            unitModel.TitleSprite.ObserveEveryValueChanged(x => x.Value)
                .Subscribe(_ =>
                {
                    CharacterSpriteRenderer.sprite = _;
                }).AddTo(this);
            
            
            unitModel.CurrentDirection.ObserveEveryValueChanged(x => x.Value)
                .Subscribe(_ =>
                {
                    if (_ == CharacterDirection.Right)
                    {
                        CharacterSpriteRenderer.flipX = false;
                    }
                    if (_ == CharacterDirection.Left)
                    {
                        CharacterSpriteRenderer.flipX = true;
                    }
                }).AddTo(this);
        }

        private void OnTakeDamageSpriteEffect(int damageAmount)
        {
            // CurrentAnimation = CharacterSpriteRenderer.DOColor(Color.red, 0.12f)
            //     .OnComplete(() =>
            //     {
            //         CharacterSpriteRenderer.color = Color.white;
            //     });
        }



        public async UniTask SpawnAnimation()
        {
            SpriteParent.transform.DOLocalMoveY(2f, 0.11f).SetEase(Ease.OutSine)
                .From();
            CharacterSpriteRenderer.transform.DOScale(new Vector3(0.2f, 2.5f, 1), 0.11f).SetEase(Ease.OutSine)
                .From();
            await UniTask.Delay(TimeSpan.FromSeconds(0.11f));
            MyVfxManager.I.CreateVfxByName("Smoke_Cloud_Burst_v1", transform.position, 0.5f);

        }
        private void OnDestroy()
        {
            CurrentAnimation?.Complete();
            CurrentAnimation?.Kill();
        }
    }
}