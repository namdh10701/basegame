using DG.Tweening;
using UnityEngine;
public class DOTweenShow : MonoBehaviour, Command
{
    View view;
    Tween showTween;
    private void Awake()
    {
        view = GetComponent<View>();
        showTween = view.canvasGroup.DOFade(1, view.duration / 2).OnComplete(() =>
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
        view.ViewState = ViewState.SHOWING;
    }

    public void Interupt()
    {

        showTween.Pause();
    }

    public void OnCompleted()
    {
        view.onShowEnd?.Invoke();
        view.ViewState = ViewState.SHOW;
    }
}