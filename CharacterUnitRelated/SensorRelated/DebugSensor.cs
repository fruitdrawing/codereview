using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace DefaultNamespace.MyScripts
{
    public class DebugSensor : MonoBehaviour
    {
        public CircleCollider2D CircleCollider2D;
        
        private void Awake()
        {
            CircleCollider2D.OnTriggerEnter2DAsObservable()
                .Subscribe(_ =>
                {
                    Debug.Log($"<color=white>{_.gameObject.name}</color>");
                }).AddTo(this);
        }
    }
}