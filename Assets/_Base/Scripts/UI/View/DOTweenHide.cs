using DG.Tweening;
using UnityEngine;

namespace _Base.Scripts.UI.Viewx
{
    public class DOTweenHide : MonoBehaviour, ICommand
    {
        View view;
        DG.Tweening.Tween hideTween;
        private void Awake()
        {
            view = GetComponent<View>();

            hideTween = DOTweenModuleUI.DOFade((CanvasGroup)view.canvasGroup, 0, view.duration / 2).OnPlay(() =>
            {
                view.canvasGroup.blocksRaycasts = false;
                view.canvasGroup.alpha = 1;
            }).OnComplete(() =>
            {
                view.root.anchoredPosition = view.originalPos;
                view.canvasGroup.alpha = 1;
                this.OnCompleted();
            });
            hideTween.SetAutoKill(false);
            hideTween.Pause();
        }
        public void Execute()
        {
            hideTween.Restart();
            view.onHideStart?.Invoke();
            view.ViewState = ViewState.Hiding;
        }

        public void Interrupt()
        {
            hideTween.Pause();
        }

        public void OnCompleted()
        {
            view.onHideEnd?.Invoke();
            view.ViewState = ViewState.Hide;
        }
    }
}