using UnityEngine;
using DG.Tweening;
public class DOTweenHide : MonoBehaviour, Command
{
    View view;
    Tween hideTween;
    private void Awake()
    {
        view = GetComponent<View>();

        hideTween = view.canvasGroup.DOFade(0, view.duration / 2).OnPlay(() =>
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
        view.ViewState = ViewState.HIDING;
    }

    public void Interupt()
    {
        hideTween.Pause();
    }

    public void OnCompleted()
    {
        view.onHideEnd?.Invoke();
        view.ViewState = ViewState.HIDE;
    }
}