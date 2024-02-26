using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace DefaultNamespace.MyScripts.VfxRelated
{
    public class MyTrailRendererHelper : MonoBehaviour
    {
        public TrailRenderer TrailRenderer;
        public void Start()
        {
            this.OnDisableAsObservable().Subscribe(_ =>
            {
                TrailRenderer.Clear();
            }).AddTo(this);
        }   
    }
}