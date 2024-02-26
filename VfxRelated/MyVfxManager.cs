using DefaultNamespace.MyScripts.AudioRelated;
using DefaultNamespace.MyScripts.UtilClasses;
using DG.Tweening;
using NaughtyAttributes;
using Redcode.Pools;
using Shapes;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.MyScripts.VfxRelated
{
    public class MyVfxManager : Singleton<MyVfxManager>
    {
        public PoolManager PoolManager;

        public Image WhiteFlash;
        public GameObject FightStartTEXT;
        
        [Button]
        public void Quick_DrawCircleTo()
        {
            DrawCircleTo(Vector3.zero,2f ,1);
        }
        
        


        [Button]
        public void TriggerFightText()
        {
            FightStartTEXT.SetActive(true);
            DOVirtual.DelayedCall(0.7f, () => FightStartTEXT.SetActive(false));
            MessageBroker.Default.Publish<PlaySfxEvent>(new PlaySfxEvent("Fight_02_Female3"));
            TriggerWhiteFlash();
            
        }
        [Button]
        public void TriggerWhiteFlash()
        {
            WhiteFlash.gameObject.SetActive(true);
            WhiteFlash.DOFade(0, 0.11f).From(0.7f).SetEase(Ease.OutSine).OnComplete(() => WhiteFlash.gameObject.SetActive(false));
        }
        
        
        public Transform CreateVfxByName(string vfxName, Vector3 targetPosition, float duration)
        {
            var pool = PoolManager.GetPool<Transform>(vfxName);
            var spawnedVFX = pool.Get();
            if (spawnedVFX == null) return null;
            
            spawnedVFX.position = targetPosition;
            DOVirtual.DelayedCall(duration, () => pool.Take(spawnedVFX));
            return spawnedVFX;
        }
        public void DrawCircleTo(Vector3 targetPosition,float radius,float duration)
        {
            var pool = PoolManager.GetPool<Transform>("VFX_CircleAttackRange");
            var spawnedCircle = pool.Get();
            
            spawnedCircle.position = targetPosition;
            spawnedCircle.GetComponent<Disc>().Radius = radius;
            DOVirtual.DelayedCall(duration, () => pool.Take(spawnedCircle));
        }

        public void DrawLineFromTo(Vector3 fromPosition,Vector3 targetPosition,float duration)
        {
            var pool = PoolManager.GetPool<Transform>("ShapeLineDraw");
            var spawnedLine = pool.Get();
            
            var line = spawnedLine.GetComponent<Line>();
            
            line.Start = fromPosition;
            line.End = targetPosition;
            
            DOVirtual.DelayedCall(duration, () => pool.Take(spawnedLine));
        }


        [Button]
        public void RightShootArrowTest()
        {
            ShootArrow(Vector3.zero, Vector3.right * 10, 0.5f);
        } 
        [Button]
        public void MeleeTest()
        {
            CreateVfxByName("Blue Slash v16", Vector3.zero, 1f);
        } 
        
        [Button]
        public void LeftShootArrowTest()
        {
            ShootArrow(Vector3.zero, Vector3.left * 10, 0.5f);
        }
        
        
        
        public void ShootArrow(Vector3 from, Vector3 to,float duration)
        {
            AudioManager.I.PlaySfx("Whoosh 8");
            var pool = PoolManager.GetPool<Transform>("Arrow");
            var spawnedArrow = pool.Get();
            if(spawnedArrow == null) return;
            spawnedArrow.position = from;

            var direction = to - from;

            Tween a;
            if (direction.x > 0)
            {
                a = spawnedArrow.DOLocalRotate(new Vector3(0, 0, -120), duration);
            }
            else
            {
                a = spawnedArrow.DOLocalRotate(new Vector3(0, 0, 120), duration);
            }
            spawnedArrow.DOJump(to, 1.3f, 1, 0.5f)
                .SetEase(Ease.InOutSine).OnComplete(() =>
                {
                    a.Complete();
                    spawnedArrow.localRotation = Quaternion.identity;
                    pool.Take(spawnedArrow);
                });
        }
        
        //
        // [Button]
        // public void PoolManagerTest()
        // {
        // }

      

     
    }
}