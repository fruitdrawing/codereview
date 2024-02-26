using System.Collections.Generic;
using DefaultNamespace.MyScripts.SharedData;
using DefaultNamespace.MyScripts.UtilClasses;
using DefaultNamespace.MyScripts.VfxRelated;
using NaughtyAttributes;
using SharedData;
using UnityEngine;
using UnityEngine.VFX;
using UtilClasses;

namespace DefaultNamespace.MyScripts
{
    public class SkillAimHelper : Singleton<SkillAimHelper>
    {
        
        
        [Button]
        public void TestCenterGrabBox()
        {
            var allCore = GetCharactersTwoDimensionInBox(Vector3.zero,new Vector2(3,3),0,TagHelper.AllTeams(),null);
            foreach (var VARIABLE in allCore)
            {
                Debug.Log($"<color=white>{VARIABLE.UnitModel.OwnerName.Value}</color>");
                VARIABLE.GetTakeDamageHelper().TakeDamage(new DamageData(10,3,0.5f));
            }
        }


        public CharacterCore[] GetCharacterCoreInCircle
        (Vector3 targetPosition, 
            float radius,
            string[] tagNames,
            CharacterCore caster,
            PivotType pivotType = PivotType.Center)
        {
            Collider2D[] collider2Ds = new Collider2D[100];

            Physics2D.OverlapCircleNonAlloc(
                targetPosition,
                radius, collider2Ds,layerMask:LayerMask.GetMask("Unit"));
 
            MyVfxManager.I.DrawCircleTo(targetPosition,radius,0.3f);
            var characterCores = new List<CharacterCore>();

            foreach (var VARIABLE in collider2Ds)
            {
                if (VARIABLE != null)
                {
                    
                    characterCores.Add(VARIABLE.GetComponent<CharacterCore>());
                }
            }

            return characterCores.ToArray();
        }
        
         public CharacterCore[] GetCharactersTwoDimensionInBox
            (Vector3 targetPosition, Vector2 boxSize, 
            float rotationZ,
            string[] tagNames,
            CharacterCore caster,
            PivotType pivotType = PivotType.Center)
        {
            var result = MyUtilManager.CalculateOriginOfPivot(pivotType, boxSize, targetPosition);
            Collider2D[] collider2Ds = new Collider2D[100];
         
            Physics2D.OverlapBoxNonAlloc(
                result.position, 
                result.size,
                rotationZ,collider2Ds,
                layerMask:LayerMask.GetMask("Unit"));
            // var c2d = Physics2D.OverlapBoxAll(result.position, result.size,rotationZ,layerMask:MyGameUtil.GetTakeDamageLayerMask());
            // Physics2D.OverlapBoxNonAlloc(result.position, result.size,rotationZ,c2d);
            // ! old
            // var c2d = Physics2D.OverlapBoxAll(pivot, boxSize,rotationZ);
            
            // if(SL<IMyVFXManager>.IsValid)
            //     SL<IMyVFXManager>.I.Debug_DrawBoxForDebug(result.position,result.size,1f);

            // float heightDifference = GameBalanceManager/**/.I.DefaultHeightDifference;
            // if (Mathf.Approximately(customHeightDifference, 0) == false)
            // {
                // heightDifference = customHeightDifference;
            // }    
            
            
            
            var characterCores = new List<CharacterCore>();

            Debug.Log($"<color=white>collider2Ds.Length : {collider2Ds.Length}</color>");
            if (collider2Ds.Length > 0)
            {
                foreach (var VARIABLE in collider2Ds)
                {
                    if(VARIABLE != null)
                        characterCores.Add(VARIABLE.gameObject.GetComponent<CharacterCore>());
                }

                return characterCores.ToArray();

                // var gameObjects = collider2Ds.OfType<Collider2D>()
                //     .Where(_=>_ != null)
                //     .Where(_ =>
                //         _.CompareAnyTags(tagNames))
                //     
                //     .Select(_ => { return GetIDamageableByTag(_); })
                //     .Where(_=>
                //     {
                //         if (_ == null) return false;
                //     // if (customHeight > 0)
                //     // {
                //         // return SL<IMySharedGameUtil>.I.CompareYHeightGapBetweenTwoValue(customHeight,
                //             // _.GetCurrentYPosition(),_.GetHeight());
                //     // }
                //     // else
                //     // {
                //         // return SL<IMySharedGameUtil>.I.CompareYHeightGapBetweenTwoValue(caster.GetCurrentYPosition(),
                //         //     _.GetCurrentYPosition(),_.GetHeight());
                //     // }
                //
                // });

                //
                // if (gameObjects != null)
                // {
                //     
                // }
                // if (gameObjects.Any())
                // {
                //     return gameObjects.Select(_=>_).ToArray();
                // }
            }
            return characterCores.ToArray();
        }
         
         
         

    }
}