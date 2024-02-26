using DefaultNamespace.MyScripts.SharedData;
using NaughtyAttributes;
using SharedData;
using UniRx;
using UnityEngine;
using UnityEngine.Events;


namespace DefaultNamespace.MyScripts
{
    public class UnitModel : MonoBehaviour
    {
        public ReactiveProperty<Sprite> TitleSprite = new();
        
        public ReactiveProperty<string> OwnerName = new("DefaultName");
        
        public ReactiveProperty<int> CurrentHp = new();
        
        public ReactiveProperty<int> MaxHp = new();

        public ReactiveProperty<float> HPRatio = new(1);
        
        public ReactiveProperty<CharacterDirection> CurrentDirection = new();

        public ReactiveProperty<UnitTeam> CurrentTeam = new();

        public ReactiveProperty<CharacterState> CurrentCharacterState = new(CharacterState.Idle);

        public ReactiveProperty<int> AttackPower = new();
        public ReactiveProperty<float> PushPower = new();
        public ReactiveProperty<float> PushDuration = new();
        
        public ReactiveProperty<float> MovementSpeed = new();
        
        public ReactiveProperty<bool> DisableRVO = new();
        
        public ReactiveProperty<bool> DisableMovement = new();
        
        public AttackType CurrentAttackType;
        
        [Space(20)] 
        public ReactiveProperty<float> CharacterGraphicHeight = new();
        
        [Space(20)] 
        public ReactiveProperty<float> AttackRange = new(3);
        public ReactiveProperty<float> DetectEnemyUnitRange = new(6);
        public ReactiveProperty<float> UnitBodyRadius = new(2);

        public UnityEvent<UnitSpawnData> OnUnitSpanwed = new();
        
        private void Awake()
        {
           
            CurrentHp.ObserveEveryValueChanged(x => x.Value)
                .Subscribe(_ =>
                {
                    if (MaxHp.Value == 0) return;
                    float ratio = (float)((float)_ / (float)MaxHp.Value);
                    HPRatio.Value = ratio;
                    
                }).AddTo(this);
            
            
            // less max hp
            CurrentHp.ObserveEveryValueChanged(x => x.Value)
                .Subscribe(_ =>
                {
                    if (_ >= MaxHp.Value)
                    {
                        CurrentHp.Value = MaxHp.Value;
                    }
                    else if (_ <= 0)
                    {
                        CurrentHp.Value = 0;
                    }
                }).AddTo(this);
            
        }

        public void InitByUnitSO(UnitSpawnData unitSpawnData)
        {
            CurrentTeam.Value = unitSpawnData.Team;
            OwnerName.Value = unitSpawnData.UnitSO.UnitName;
            MaxHp.Value = unitSpawnData.UnitSO.MaxHp;
            CurrentHp.Value = unitSpawnData.UnitSO.InitialHp;
            AttackRange.Value = unitSpawnData.UnitSO.AttackRange;
            DetectEnemyUnitRange.Value = unitSpawnData.UnitSO.DetectEnemyUnitRange;
            TitleSprite.Value = unitSpawnData.UnitSO.UnitSprite;
            CharacterGraphicHeight.Value = unitSpawnData.UnitSO.CharacterGraphicHeight;
            CurrentAttackType = unitSpawnData.UnitSO.AttackType;
            MovementSpeed.Value = unitSpawnData.UnitSO.MoveSpeed;
            DisableMovement.Value = unitSpawnData.UnitSO.DisableMovement;
            DisableRVO.Value = unitSpawnData.UnitSO.DisableRVO;
            UnitBodyRadius.Value = unitSpawnData.UnitSO.Radius;
            AttackPower.Value = unitSpawnData.UnitSO.AttackPower;
            PushDuration.Value = unitSpawnData.UnitSO.PushDuration;
            PushPower.Value = unitSpawnData.UnitSO.PushPower;
            
            OnUnitSpanwed?.Invoke(unitSpawnData);
        }
        
        [Button]
        
        public void Debug_FlipDirection()
        {
            if (CurrentDirection.Value == CharacterDirection.Left)
            {
                CurrentDirection.Value = CharacterDirection.Right;
            }  
            else if (CurrentDirection.Value == CharacterDirection.Right)
            {
                CurrentDirection.Value = CharacterDirection.Left;
            }
        }

        public DamageData CreateDamageDataByUnitModel()
        {
            var dd = new DamageData() { };
            dd.DamageAmount = AttackPower.Value;
            dd.PushPower = PushPower.Value;
            dd.PushDuration = PushDuration.Value;
            
            return dd;
        }
    }
}