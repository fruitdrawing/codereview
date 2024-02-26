using System;
using DefaultNamespace.MyScripts.GameData;
using DefaultNamespace.MyScripts.VfxRelated;
using NaughtyAttributes;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace DefaultNamespace.MyScripts.TestRelated
{
    public class MMMMMTEST : MonoBehaviour
    {
        public Collider2D Collider2D;

        [Button]
        public void GETINSINDE()
        {
            TrailRenderer trailRenderer;
            var foundpoint = RandomPointInBounds(Collider2D.bounds);
            MyVfxManager.I.DrawCircleTo(foundpoint, 0.5f,  0.5f);
        }
        public static Vector3 RandomPointInBounds(Bounds bounds) {
            return new Vector3(
                UnityEngine.Random.Range(bounds.min.x, bounds.max.x),
                UnityEngine.Random.Range(bounds.min.y, bounds.max.y),
                UnityEngine.Random.Range(bounds.min.z, bounds.max.z)
            );
        }

        private void Awake()
        {
            Application.targetFrameRate = 60;
            GameOptionDB.I.ShowNameTextTMP.ObserveEveryValueChanged(x => x.Value)
                .Subscribe(_ =>
                {
                    Debug.Log($"<color=white>{_}</color>");
                }).AddTo(this);

            this.UpdateAsObservable().ThrottleFirst(TimeSpan.FromSeconds(1))
                .Subscribe(_ =>
                {
                    Debug.Log($"<color=white>SAMPLEFRAME</color>");
                }).AddTo(this);
        }

        private void Update()
        {
        }
    }
}