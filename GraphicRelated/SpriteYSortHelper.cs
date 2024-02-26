using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace DefaultNamespace.MyScripts.GraphicRelated
{
    public class SpriteYSortHelper : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        public Transform FollowTransform;
        
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            FollowSpriteOrder();
        }

        private void FollowSpriteOrder()
        {
            this.UpdateAsObservable().Subscribe(_ =>
            {
                _spriteRenderer.sortingOrder = -(int)(FollowTransform.position.y * 100);
            }).AddTo(this);
        }
    }
}