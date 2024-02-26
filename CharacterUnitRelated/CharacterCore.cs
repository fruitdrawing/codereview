using DefaultNamespace.MyScripts.InputRelated;
using DG.Tweening;
using NaughtyAttributes;
using SharedData;
using UniRx;
using UnityEngine;

namespace DefaultNamespace.MyScripts
{
    public class CharacterCore : MonoBehaviour
    {
        public Rigidbody2D rigidBody2D;
        public CircleCollider2D bodyCollider2D;
        
        public UnitModel UnitModel;
        
        public DeathBehaviour DeathBehaviour;

        public SimpleInputHelper Input;

        public bool IsPlayerControllingCharacter;
        
        public TakeDamageHelper TakeDamageHelper;
        public CharacterCore InitByUnitSpawnData(UnitSpawnData unitSpawnData)
        {
            this.gameObject.tag = unitSpawnData.Team.ToString();
            UnitModel.InitByUnitSO(unitSpawnData);
            if (unitSpawnData.IsPlayerController == true)
            {
                Input.Available = true;
                IsPlayerControllingCharacter = true;
            }
            return this;
        }
        
        private void Awake()
        {
            UnitModel.UnitBodyRadius.ObserveEveryValueChanged(x => x.Value)
                .Subscribe(_ =>
                {
                    bodyCollider2D.radius = _;
                }).AddTo(this);
        }

        [Button]
        public void Debug_AddForce()
        {
            rigidBody2D.DOMove(new Vector2(2,1),0.8f);
        }

       

        public TakeDamageHelper GetTakeDamageHelper()
        {
            return this.TakeDamageHelper;
        }

      
    }
}