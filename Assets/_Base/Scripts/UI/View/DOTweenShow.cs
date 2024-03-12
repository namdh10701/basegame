using DG.Tweening;
using UnityEngine;

namespace _Base.Scripts.UI.Viewx
{
    public class DOTweenShow : MonoBehaviour, Command
    {
        View view;
        DG.Tweening.Tween showTween;
        private void Awake()
        {
            view = GetComponent<View>();
            showTween = DOTweenModuleUI.DOFade((CanvasGroup)view.canvasGroup, 1, view.duration / 2).OnComplete(() =>
            {
                view.canvasGroup.blocksRaycasts = true;
                this.OnCompleted();
            }).OnPlay(() =>
            {
                view.canvasGroup.alpha = 0;
                view.root.anchoredPosition = Vector2.zero;
            });
            showTween.SetAutoKill(false);
            showTween.Pause();
        }
        public void Execute()
        {
            view.onShowStart?.Invoke();
            showTween.Restart();
            view.ViewState = ViewState.Showing;
        }

        public void Interrupt()
        {

            showTween.Pause();
        }

        public void OnCompleted()
        {
            view.onShowEnd?.Invoke();
            view.ViewState = ViewState.Show;
        }
    }
}