using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.MyScripts.GamePlayerRelated
{
    public class GamePlayerPresenter : MonoBehaviour
    {
        public GamePlayerModel Model;
        public Image ManaBarFill;
        public Image ManaBarBg;

        private void Awake()
        {
            Model.CurrentManaRatio.ObserveEveryValueChanged(x => x.Value)
                .Subscribe(_ =>
                {
                    ManaBarFill.fillAmount = _;
                }).AddTo(this);
        }
    }
}