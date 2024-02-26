using System;
using DefaultNamespace.MyScripts.GameData;
using DefaultNamespace.MyScripts.SharedData;
using NaughtyAttributes;
using Pathfinding;
using Shapes;
using SharedData;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.MyScripts.UnitDebuggerRelated
{
    public class UnitViewDebugger : MonoBehaviour
    {
        
        [Button]
        public void ToggleAllVisual()
        {
            ShowAttackRange.Value = !ShowAttackRange.Value;
            ShowEnemyUnitDetectRange.Value = !ShowEnemyUnitDetectRange.Value;
            ShowMovingDestination.Value = !ShowMovingDestination.Value;
            ShowAttackingTargetLine.Value = !ShowAttackingTargetLine.Value;
        }


        public TextMeshPro CurrentChracterState;
        
        
        public UnitModel unitModel;
        public SensorHelper SensorHelper;



        [Header("Current Attacking Target Line")]
        public Image HPBarValue;
        public Image HPBarBG;
        
        [Header("Current Attacking Target Line")]
        public ReactiveProperty<bool> ShowAttackingTargetLine = new();
        public Line AttackingTargetLine;
        private IDisposable lineSubsicription;
        
        
        [Space(20)]
        [Header("Current AIPath Line")]
        public ReactiveProperty<bool> ShowMovingDestination = new();
        public Line MovingDestinationLine;
        public AIPath Aipath;
        
        [Space(20)]
        [Header("Detect Range")]
        public ReactiveProperty<bool> ShowEnemyUnitDetectRange = new();
        public Disc DetectRangeDisc;
        
        [Space(20)]
        [Header("Attack Range")]
        public ReactiveProperty<bool> ShowAttackRange = new();
        public Disc AttackRangeDisc;


        private void OnDestroy()
        {
            lineSubsicription?.Dispose();
        }

        private void Awake()
        {
            unitModel.CurrentTeam.ObserveEveryValueChanged(x => x.Value)
                .Subscribe(_ =>
                {
                    
                    if (unitModel.CurrentTeam.Value == UnitTeam.PlayerTeam)
                    {
                        HPBarValue.color = GraphicDB.I.ColorDictionary[MyColor.FriendlyHP].Value;
                        HPBarBG.color = GraphicDB.I.ColorDictionary[MyColor.FridenlyHPBG].Value;
                    }
                    else
                    {
                        HPBarValue.color = GraphicDB.I.ColorDictionary[MyColor.EnemyHP].Value;
                        HPBarBG.color = GraphicDB.I.ColorDictionary[MyColor.EnemyHpBG].Value;
                    }

                }).AddTo(this);
            
            if (GameOptionDB.I.ShowUnitDebugInfo == true)
            {
                ToggleAllVisual();
            }

            GameOptionDB.I.ShowHPBar.ObserveEveryValueChanged(x => x.Value)
                .Subscribe(_ =>
                {
                    HPBarBG.gameObject.SetActive(_);
                }).AddTo(this);
            
            unitModel.HPRatio.ObserveEveryValueChanged(x => x.Value)
                .Subscribe(_ =>
                {
                    HPBarValue.fillAmount = _;
                }).AddTo(this);

            AttackingTargetLine.gameObject.SetActive(false);
            
            SensorHelper.OnAttackingTargetChanged.AsObservable().Subscribe(target =>
                {
                    if (target != null)
                    {
                        if (AttackingTargetLine == null) return;
                        AttackingTargetLine.gameObject.SetActive(true);
                        lineSubsicription?.Dispose();
                        lineSubsicription = this.UpdateAsObservable().Subscribe(hoho =>
                        {
                            if(target != null)
                                AttackingTargetLine.End  = new Vector3(target.transform.position.x-this.transform.position.x, target.transform.position.y-this.transform.position.y,0);
                            AttackingTargetLine.DashOffset += Time.deltaTime * 2;
                        }).AddTo(this);
                       
                    }
                    else
                    {
                        AttackingTargetLine.gameObject.SetActive(false);
                        if (lineSubsicription != null) lineSubsicription.Dispose();
                    }
                }).AddTo(this);
            
            
            ShowAttackRange.ObserveEveryValueChanged(x => x.Value)
                .Subscribe(_ =>
                {
                    AttackRangeDisc.gameObject.SetActive(_);
                }).AddTo(this);
            
            ShowMovingDestination.ObserveEveryValueChanged(x => x.Value)
                .Subscribe(_ =>
                {
                    MovingDestinationLine.gameObject.SetActive(_);
                }).AddTo(this);
            
            ShowAttackingTargetLine.ObserveEveryValueChanged(x => x.Value)
                .Subscribe(_ =>
                {
                    AttackingTargetLine.gameObject.SetActive(_);
                }).AddTo(this);
            
            
            ShowEnemyUnitDetectRange.ObserveEveryValueChanged(x => x.Value)
                .Subscribe(_ =>
                {
                    DetectRangeDisc.gameObject.SetActive(_);
                }).AddTo(this);
            
            
            unitModel.AttackRange.ObserveEveryValueChanged(x => x.Value)
                .Subscribe(_ =>
                {
                    AttackRangeDisc.Radius = _;
                }).AddTo(this);
            
            
            unitModel.DetectEnemyUnitRange.ObserveEveryValueChanged(x => x.Value)
                .Subscribe(_ =>
                {
                    DetectRangeDisc.Radius = _;
                }).AddTo(this);
        }
    }
}